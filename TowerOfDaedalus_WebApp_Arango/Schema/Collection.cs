using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;

namespace TowerOfDaedalus_WebApp_Arango.Schema
{
    public class Collection
    {
        public string Name { get; set; }
        public bool WaitForSync { get; set; }
        public ArangoDBNetStandard.CollectionApi.Models.CollectionType Type { get; set; }

        public Collection(string name, bool waitForSync = false, 
            ArangoDBNetStandard.CollectionApi.Models.CollectionType type = ArangoDBNetStandard.CollectionApi.Models.CollectionType.Document)
        {
            Name = name;
            WaitForSync = waitForSync;
            Type = type;
        }
    }
}
