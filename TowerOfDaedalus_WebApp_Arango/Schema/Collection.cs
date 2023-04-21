using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;

namespace TowerOfDaedalus_WebApp_Arango.Schema
{
    /// <summary>
    /// Class that defines an Arango collection
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// The name of the collection
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// whether arango should wait to send a response until the collection is committed
        /// </summary>
        public bool WaitForSync { get; set; }

        /// <summary>
        /// The type of collection
        /// </summary>
        public ArangoDBNetStandard.CollectionApi.Models.CollectionType Type { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="name">the name of the collection</param>
        /// <param name="waitForSync">whether arango should wait to send a response until the collection is committed</param>
        /// <param name="type">the type of collection</param>
        public Collection(string name, bool waitForSync = false, 
            ArangoDBNetStandard.CollectionApi.Models.CollectionType type = ArangoDBNetStandard.CollectionApi.Models.CollectionType.Document)
        {
            Name = name;
            WaitForSync = waitForSync;
            Type = type;
        }
    }
}
