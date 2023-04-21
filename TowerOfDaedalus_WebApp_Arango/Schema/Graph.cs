using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfDaedalus_WebApp_Arango.Schema
{
    /// <summary>
    /// Class defining an Arango named graph
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// The name of the graph
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// list of Edge Definitions to be added to the graph
        /// </summary>
        public List<ArangoDBNetStandard.GraphApi.Models.EdgeDefinition> EdgeDefinitions { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="name">name of the graph</param>
        /// <param name="edgeDefinitions">list of edge definitions to be added</param>
        public Graph(string name, List<ArangoDBNetStandard.GraphApi.Models.EdgeDefinition> edgeDefinitions)
        { 
            Name = name;
            EdgeDefinitions = edgeDefinitions;
        }
    }
}
