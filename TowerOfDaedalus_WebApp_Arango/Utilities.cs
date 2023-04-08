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
            bool skipCreate = false;
            _logger.LogInformation("checking for existing database");
            // Initiate the transport
            using (var transport = HttpApiTransport.UsingBasicAuth(new Uri(ArangoDbContext.getUrl()), ArangoDbContext.getSystemDbName(), ArangoDbContext.getSystemUsername(), ArangoDbContext.getSystemPassword()))
            {
                // Initiate ArangoDBClient using the transport
                using (var db = new ArangoDBClient(transport))
                {
                    var response = Task.Run(() => db.Database.GetCurrentDatabaseInfoAsync()).Result;

                    if (response.Code != System.Net.HttpStatusCode.OK)
                    {
                        _logger.LogError("Arango returned a non-ok status code");
                        throw new HttpRequestException("Could not find the ArangoDB _System database");
                    }

                    var databases = Task.Run(() => db.Database.GetDatabasesAsync()).Result;

                    skipCreate = databases.Result.Contains(ArangoDbContext.getDbName());
                    if (!skipCreate)
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
                            throw new HttpRequestException("Could not create the new database");
                        }
                    }
                }
            }
            if (!skipCreate)
            {
                using (var transport2 = HttpApiTransport.UsingBasicAuth(new Uri(ArangoDbContext.getUrl()), ArangoDbContext.getDbName(), ArangoDbContext.getNewUsername(), ArangoDbContext.getNewPass()))
                {
                    using (var db = new ArangoDBClient(transport2))
                    {
                        // Create collections
                        _logger.LogInformation("creating collections");
                        foreach (Collection item in ArangoSchema.Collections)
                        {
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
                                throw new HttpRequestException($"Could not add the {item.Name} collection to ArangoDB");
                            }
                        }

                        // Create graph
                        _logger.LogInformation("creating graph and edge definitions");
                        foreach (Graph graph in ArangoSchema.Graphs)
                        {
                            var newGraph = new ArangoDBNetStandard.GraphApi.Models.PostGraphBody()
                            {
                                Name = graph.Name,
                                EdgeDefinitions = graph.EdgeDefinitions
                            };

                            // Create the new graph
                            var graphResponse = Task.Run(() => db.Graph.PostGraphAsync(newGraph, null)).Result;

                            if (graphResponse.Code != System.Net.HttpStatusCode.Accepted)
                            {
                                _logger.LogError("Arango returned a non-ok status code");
                                throw new HttpRequestException($"Could not add the {graph.Name} graph to ArangoDb");
                            }
                        }
                    }
                }
            }
        }
    }
}

