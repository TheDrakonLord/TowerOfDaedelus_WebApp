using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.Transport.Http;
using TowerOfDaedalus_WebApp_Arango.Schema;

namespace TowerOfDaedalus_WebApp_Arango
{
    /// <summary>
    /// Interfance for defining arango utility classes
    /// </summary>
    public interface IArangoUtils
    {
        //public static abstract Task CreateDB();
    }

    /// <summary>
    /// Class for establishing and maintaining an arango database
    /// </summary>
    public class Utilities : IArangoUtils
    {
        private readonly ILogger _logger;
        private const string dbName = "PrimaryDB";
        private const string systemDbName = "_system";
        private const string systemUsername = "-----";
        private const string systemPassword = "-----";
        private const string url = "http://localhost:8529/";
        private const string newUsername = "todWebApp";
        private const string newPass = "todWebAppPwd";
        
        /// <summary>
        /// Default constructor for arango utilities
        /// </summary>
        /// <param name="logger">logging provider for logging messages</param>
        public Utilities(ILogger logger)
        {
            _logger = logger;
            Task.Run(() => CreateDB()).Wait();
        }

        /// <summary>
        /// Checks if the DB exists, If it exists we do nothing.
        /// Otherwise, we create the database, collections, and graph with edge definitions
        /// </summary>
        /// <returns>task completion status</returns>
        /// <exception cref="HttpRequestException">This exception is thrown if the https requests return anything but OK</exception>
        public static async Task CreateDB()
        {
            // Initiate the transport
            using (var transport = HttpApiTransport.UsingBasicAuth(new Uri(url), systemDbName, systemUsername, systemPassword))
            {
                // Initiate ArangoDBClient using the transport
                using (var db = new ArangoDBClient(transport))
                {
                    var response = await db.Database.GetCurrentDatabaseInfoAsync();

                    if (response.Code != System.Net.HttpStatusCode.OK)
                    {
                        throw new HttpRequestException("Could not find the ArangoDB _System database");
                    }

                    var databases = await db.Database.GetDatabasesAsync();

                    if (!databases.Result.Contains(dbName))
                    {
                        // Define the new database and its users
                        var body = new ArangoDBNetStandard.DatabaseApi.Models.PostDatabaseBody()
                        {
                            Name = dbName,
                            Users = new List<ArangoDBNetStandard.DatabaseApi.Models.DatabaseUser>()
                            {
                                new ArangoDBNetStandard.DatabaseApi.Models.DatabaseUser()
                                {
                                    Username=newUsername,
                                    Passwd =newPass,
                                    Active=true
                                }
                            }
                        };

                        // Create the new database
                        var newDBResponse = await db.Database.PostDatabaseAsync(body);

                        if (newDBResponse.Code != System.Net.HttpStatusCode.OK)
                        {
                            throw new HttpRequestException("Could not create the new database");
                        }

                        // Create collections
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
                            var collResponse = await db.Collection.PostCollectionAsync(newColl, null);

                            if (collResponse.Code != System.Net.HttpStatusCode.OK)
                            {
                                throw new HttpRequestException($"Could not add the {item.Name} collection to ArangoDB");
                            }
                        }

                        // Create graph
                        foreach (Graph graph in ArangoSchema.Graphs)
                        {
                            var newGraph = new ArangoDBNetStandard.GraphApi.Models.PostGraphBody()
                            {
                                Name = graph.Name,
                                EdgeDefinitions = graph.EdgeDefinitions
                            };

                            // Create the new graph
                            var graphResponse = await db.Graph.PostGraphAsync(newGraph, null);

                            if (graphResponse.Code != System.Net.HttpStatusCode.OK)
                            {
                                throw new HttpRequestException($"Could not add the {graph.Name} graph to ArangoDb");
                            }
                        }
                    }
                }
            }
        }
    }
}
