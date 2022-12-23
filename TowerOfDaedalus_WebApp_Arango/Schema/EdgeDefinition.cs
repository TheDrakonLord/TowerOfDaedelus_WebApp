using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfDaedalus_WebApp_Arango.Schema
{
    public class EdgeDefinition
    {
        public Collection collection { get; set; }
        public List<Collection> from { get; set; }
        public List<Collection> to { get; set; }
        public EdgeDefinition(Collection collection, List<Collection> from, List<Collection> to) 
        {
            this.collection = collection;
            this.from = from;
            this.to = to;
        }
    }
}
