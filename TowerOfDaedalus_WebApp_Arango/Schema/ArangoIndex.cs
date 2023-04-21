using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.IndexApi.Models;

namespace TowerOfDaedalus_WebApp_Arango.Schema
{
    /// <summary>
    /// Defines an index to index a collection with
    /// </summary>
    public class ArangoIndex
    {
        private PostIndexQuery query;
        private PostIndexBody body;
        private List<string>? fields;

        /// <summary>
        /// default constructor
        /// </summary>
        public ArangoIndex()
        {
            query = new PostIndexQuery();
            body = new PostIndexBody();
        }

        /// <summary>
        /// Name of the collection
        /// </summary>
        public string CollectionName
        {
            get { return query.CollectionName; }
            set { query.CollectionName = value; }
        }

        /// <summary>
        /// Adds a Field to be indexed
        /// The attributes to be indexed. Depending on the index type, a single attribute or multiple attributes can be indexed. 
        /// </summary>
        /// <param name="fieldName">Name of the field to index</param>
        public void AddField(string fieldName)
        {
            fields.Add(fieldName);
            body.Fields = fields;
        }

        /// <summary>
        /// The attributes to be indexed. Depending on the index type, a single attribute or multiple attributes can be indexed. 
        /// </summary>
        public IEnumerable<string> Fields
        {
            get { return body.Fields; }
            set { body.Fields= value; }
        }

        /// <summary>
        /// Can be set to true to create the index in the background, which will not write-lock the underlying collection for as long as if the index is built in the foreground. 
        /// </summary>
        public bool? InBackground
        {
            get { return body.InBackground; }
            set { body.InBackground = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return body.Name; }
            set { body.Name = value; }
        }

        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-fulltext.html 
        /// </summary>
        public void SetTypeFullText()
        {
            body.Type = IndexTypes.Fulltext;
        }

        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-geo.html 
        /// </summary>
        public void SetTypeGeo()
        {
            body.Type = IndexTypes.Geo;
        }

        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-persistent.html 
        /// </summary>
        public void SetTypePersistent()
        {
            body.Type = IndexTypes.Persistent;
        }

        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-ttl.html 
        /// </summary>
        public void SetTypeTTL()
        {
            body.Type = IndexTypes.TTL;
        }

        /// <summary>
        /// The type of index to create. Supported index types can be found in IndexTypes. 
        /// </summary>
        public string Type 
        { 
            get { return body.Type; } 
            set { body.Type = value; }
        }

        /// <summary>
        /// The body of the post index request
        /// </summary>
        public PostIndexBody Body { get { return body; } }

        /// <summary>
        /// the query of the post index request
        /// </summary>
        public PostIndexQuery Query { get { return query; } }
    }
}
