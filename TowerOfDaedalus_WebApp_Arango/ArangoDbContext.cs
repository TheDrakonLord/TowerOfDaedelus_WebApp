using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.DocumentApi.Models;
using ArangoDBNetStandard.GraphApi.Models;
using ArangoDBNetStandard.Transport.Http;
using static TowerOfDaedalus_WebApp_Arango.Schema.Documents;
using TowerOfDaedalus_WebApp_Arango.Schema;

namespace TowerOfDaedalus_WebApp_Arango
{
    public static class ArangoDbContext
    {
        private static string dbName;
        private static string systemDbName;
        private static string systemUsername;
        private static string systemPassword;
        private static string url;
        private static string newUsername;
        private static string newPass;

        public static void setEnvVariables()
        {
            dbName = Environment.GetEnvironmentVariable("ARANGO_DB_NAME");
            systemDbName = Environment.GetEnvironmentVariable("ARANGO_SYSTEM_DB_NAME");
            systemUsername = Environment.GetEnvironmentVariable("ARANGO_SYSTEM_USER_NAME");
            systemPassword = Environment.GetEnvironmentVariable("ARANGO_SYSTEM_PASSWORD");
            url = Environment.GetEnvironmentVariable("ARANGO_URL");
            newUsername = Environment.GetEnvironmentVariable("ARANGO_NEW_USERNAME");
            newPass = Environment.GetEnvironmentVariable("ARANGO_NEW_PASSWORD");
        }

        public static string getDbName() { return dbName; }
        public static string getSystemDbName() { return systemDbName; }
        public static string getSystemUsername() { return systemUsername; }
        public static string getSystemPassword() { return systemPassword; }
        public static string getUrl() { return url; }
        public static string getNewUsername() { return newUsername; }
        public static string getNewPass() { return newPass; }
    }
}
