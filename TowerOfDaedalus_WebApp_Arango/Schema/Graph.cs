using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfDaedalus_WebApp_Arango.Schema
{
    public class Graph
    {
        public string Name { get; set; }
        public List<ArangoDBNetStandard.GraphApi.Models.EdgeDefinition> EdgeDefinitions { get; set; }
        public Graph(string name, List<ArangoDBNetStandard.GraphApi.Models.EdgeDefinition> edgeDefinitions)
        { 
            Name = name;
            EdgeDefinitions = edgeDefinitions;
        }
    }
}
