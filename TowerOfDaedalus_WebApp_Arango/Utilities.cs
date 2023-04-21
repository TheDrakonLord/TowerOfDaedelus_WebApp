using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.Transport.Http;
using TowerOfDaedalus_WebApp_Arango.Schema;
using Microsoft.Extensions.Logging;
using ArangoDBNetStandard.IndexApi.Models;
using ArangoDBNetStandard.GraphApi.Models;

namespace TowerOfDaedalus_WebApp_Arango
{
    /// <summary>
    /// Interfance for defining arango utility classes
    /// </summary>
    public interface IArangoUtils
    {
        /// <summary>
        /// Checks if the DB exists, If it exists we do nothing.
        /// Otherwise, we create the database, collections, and graph with edge definitions
        /// </summary>
        /// <returns>task completion status</returns>
        void CreateDB();
    }

    /// <summary>
    /// Class for establishing and maintaining an arango database
    /// </summary>
    public class Utilities : IArangoUtils
    {
        private static ILogger<Utilities> _logger;
        private static bool _created = false;
        private static Mutex _mutex = new Mutex();


        /// <summary>
        /// Default constructor for arango utilities
        /// </summary>
        /// <param name="logger">logging provider for logging messages</param>
        public Utilities(ILogger<Utilities> logger)
        {
            _logger = logger;

            _logger.LogDebug("retrieving database environment variables");
            ArangoDbContext.setEnvVariables();

            _logger.LogDebug("lauching createDB task");
            CreateDB();
        }

        /// <summary>
        /// Checks if the DB exists, If it exists we do nothing.
        /// Otherwise, we create the database, collections, and graph with edge definitions
        /// </summary>
        /// <returns>task completion status</returns>
        /// <exception cref="HttpRequestException">This exception is thrown if the https requests return anything but OK</exception>
        public void CreateDB()
        {
            _mutex.WaitOne();
            if (!_created)
            {
                _logger.LogInformation("checking for existing database");
                // Initiate the transport
                using (var systemTransport = HttpApiTransport.UsingBasicAuth(new Uri(ArangoDbContext.getUrl()), ArangoDbContext.getSystemDbName(), ArangoDbContext.getSystemUsername(), ArangoDbContext.getSystemPassword()))
                {
                    // Initiate ArangoDBClient using the systemTransport
                    using (var db = new ArangoDBClient(systemTransport))
                    {
                        var response = Task.Run(() => db.Database.GetCurrentDatabaseInfoAsync()).Result;

                        if (response.Code != System.Net.HttpStatusCode.OK)
                        {
                            _logger.LogError("Arango returned a non-ok status code");
                            _mutex.ReleaseMutex();
                            throw new HttpRequestException("Could not find the ArangoDB _System database");
                        }

                        var databases = Task.Run(() => db.Database.GetDatabasesAsync()).Result;

                        if (!databases.Result.Contains(ArangoDbContext.getDbName()))
                        {
                            _logger.LogInformation("Database not found, creating new database");
                            // Define the new database and its users
                            var body = new ArangoDBNetStandard.DatabaseApi.Models.PostDatabaseBody()
                            {
                                Name = ArangoDbContext.getDbName(),
                                Users = new List<ArangoDBNetStandard.DatabaseApi.Models.DatabaseUser>()
                            {
                                new ArangoDBNetStandard.DatabaseApi.Models.DatabaseUser()
                                {
                                    Username=ArangoDbContext.getNewUsername(),
                                    Passwd =ArangoDbContext.getNewPass(),
                                    Active=true
                                }
                            }
                            };

                            // Create the new database
                            var newDBResponse = Task.Run(() => db.Database.PostDatabaseAsync(body)).Result;

                            if (newDBResponse.Code != System.Net.HttpStatusCode.Created)
                            {
                                _logger.LogError("Arango returned a non-ok status code");
                                _mutex.ReleaseMutex();
                                throw new HttpRequestException("Could not create the new database");
                            }
                        }
                    }
                }

                using (var primaryTransport = HttpApiTransport.UsingBasicAuth(new Uri(ArangoDbContext.getUrl()), ArangoDbContext.getDbName(), ArangoDbContext.getNewUsername(), ArangoDbContext.getNewPass()))
                {
                    using (var db = new ArangoDBClient(primaryTransport))
                    {
                        _logger.LogInformation("Getting list of all collections");
                        var existCollResponse = Task.Run(() => db.Collection.GetCollectionsAsync()).Result.Result;

                        // Create collections
                        _logger.LogInformation("creating collections");
                        foreach (Collection item in ArangoSchema.Collections)
                        {
                            if (!existCollResponse.Where(p => p.Name == item.Name).Any())
                            {
                                _logger.LogInformation("creating collection: {name}", item.Name);
                                // Set collection properties
                                var newColl = new ArangoDBNetStandard.CollectionApi.Models.PostCollectionBody()
                                {
                                    Type = item.Type,
                                    WaitForSync= item.WaitForSync,
                                    Name = item.Name
                                };
                                // Create the new collection
                                var collResponse = Task.Run(() => db.Collection.PostCollectionAsync(newColl, null)).Result;

                                if (collResponse.Code != System.Net.HttpStatusCode.OK)
                                {
                                    _logger.LogError("Arango returned a non-ok status code");
                                    _mutex.ReleaseMutex();
                                    throw new HttpRequestException($"Could not add the {item.Name} collection to ArangoDB");
                                }
                            }
                            else
                            {
                                _logger.LogDebug("Collection [{name}] already exists", item.Name);
                            }
                        }

                        // Create Indices
                        foreach (ArangoIndex item in ArangoSchema.indices)
                        {
                            _logger.LogInformation("Getting list of all Indices in collection [{coll}]", item.CollectionName);
                            GetAllCollectionIndexesQuery indexesQuery = new GetAllCollectionIndexesQuery();
                            indexesQuery.CollectionName = item.CollectionName;
                            var existIndexResponse = Task.Run(() => db.Index.GetAllCollectionIndexesAsync(indexesQuery)).Result.Indexes;
                            if (!existIndexResponse.Where(p => p.Name == item.Name).Any())
                            {
                                _logger.LogInformation("creating index [{index}] in collection [{coll}]", item.Name, item.CollectionName);

                                var newIndexBody = new ArangoDBNetStandard.IndexApi.Models.PostPersistentIndexBody()
                                {
                                    Sparse = true,
                                    Fields = item.Fields,
                                    InBackground = item.InBackground,
                                    Name = item.Name,
                                    Type = item.Type,
                                    Unique = true
                                };

                                var indexResponse = Task.Run(() => db.Index.PostPersistentIndexAsync(item.Query, newIndexBody)).Result;

                                if (indexResponse.Code != System.Net.HttpStatusCode.OK && indexResponse.Code != System.Net.HttpStatusCode.Created)
                                {
                                    _logger.LogError("Arango returned a non-ok status code");
                                    _mutex.ReleaseMutex();
                                    throw new HttpRequestException($"Could not add the {item.Name} index to ArangoDB");
                                }
                            }
                            else
                            {
                                _logger.LogDebug("index [{index}] already exists in collection [{coll}]", item.Name, item.CollectionName);
                            }
                        }

                        // Create graph
                        _logger.LogInformation("Getting list of all graphs");
                        var existGraphResponse = Task.Run(() => db.Graph.GetGraphsAsync()).Result.Graphs;

                        _logger.LogInformation("creating graph and edge definitions");
                        foreach (Graph graph in ArangoSchema.Graphs)
                        {
                            if (!existGraphResponse.Where(p => p.Name == graph.Name).Any())
                            {
                                _logger.LogInformation("Creating graph [{graph}]", graph.Name);
                                var newGraph = new ArangoDBNetStandard.GraphApi.Models.PostGraphBody()
                                {
                                    Name = graph.Name
                                };

                                // Create the new graph
                                var graphResponse = Task.Run(() => db.Graph.PostGraphAsync(newGraph, null)).Result;

                                if (graphResponse.Code != System.Net.HttpStatusCode.Accepted)
                                {
                                    _logger.LogError("Arango returned a non-ok status code");
                                    _mutex.ReleaseMutex();
                                    throw new HttpRequestException($"Could not add the {graph.Name} graph to ArangoDb");
                                }
                            }
                            else
                            {
                                _logger.LogDebug("the graph [{graph}] already exists", graph.Name);
                            }

                            _logger.LogInformation("Getting list of edge definitions in graph [{graph}]", graph.Name);
                            var existEdgeDefinitions = Task.Run(() => db.Graph.GetGraphAsync(graph.Name)).Result.Graph.EdgeDefinitions;
                            foreach (EdgeDefinition edge in graph.EdgeDefinitions)
                            {
                                if (!existEdgeDefinitions.Where(p => p.Collection == edge.Collection).Any())
                                {
                                    _logger.LogInformation("Creating edge [{edge}]", edge.Collection);
                                    var newEdge = new ArangoDBNetStandard.GraphApi.Models.PostEdgeDefinitionBody()
                                    {
                                        Collection = edge.Collection,
                                        From = edge.From,
                                        To = edge.To
                                    };

                                    var edgeResponse = Task.Run(() => db.Graph.PostEdgeDefinitionAsync(graph.Name, newEdge)).Result;

                                    if (edgeResponse.Code != System.Net.HttpStatusCode.Accepted)
                                    {
                                        _logger.LogError("Arango returned a non-ok status code");
                                        _mutex.ReleaseMutex();
                                        throw new HttpRequestException($"Could not add the {edge.Collection} edge to ArangoDb");
                                    }
                                }
                                else
                                {
                                    _logger.LogDebug("The edge [{edge}] already exists", edge.Collection);
                                }
                            }
                        }
                    }
                }
                _created = true;
                _mutex.ReleaseMutex();
            }
        }
    }
}

