using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfDaedalus_WebApp_Arango
{
    /// <summary>
    /// Class for composing arango query language strings using the stringbuilder class
    /// </summary>
    public class ArangoQueryBuilder
    {
        private StringBuilder sb;
        private bool limited_ = false;
        private bool filtered_ = false;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="collection">the collection to search for the document in</param>
        public ArangoQueryBuilder(string collection)
        {
            sb = new StringBuilder($"FOR doc IN {collection}");
        }

        /// <summary>
        /// adds a filter statement to the query. These statement should be used consecutively when using more than one.
        /// </summary>
        /// <param name="property">the property of the document to filter by</param>
        /// <param name="value">the value of the property we want to filter by</param>
        public void filter(string property, string value)
        {
            if (!filtered_)
            {
                sb.Append($" FILTER doc.{property} == \"{value}\"");
                filtered_ = true;
            }
            else
            {
                sb.Append($" && doc.{property} == \"{value}\"");
            }
        }

        /// <summary>
        /// adds a limit statement to the query. This statement may only be used once
        /// </summary>
        /// <param name="count">the limit to be applied</param>
        public void limit(int count)
        {
            if (!limited_)
            {
                sb.Append($" LIMIT {count}");
            }
        }

        /// <summary>
        /// adds a limit statment to the query while also providing an offset. this statment may only be used once.
        /// </summary>
        /// <param name="offset">specifies how many elements from the result shall be skipped</param>
        /// <param name="count">the limit to be applied</param>
        public void limit(int offset, int count)
        {
            if (!limited_)
            {
                sb.Append($" LIMIT {offset}, {count}");
            }
        }

        private void finish()
        {
            sb.Append(" RETURN doc");
        }

        /// <summary>
        /// Finalizes the query string and returns it.
        /// </summary>
        /// <returns>returns the finalized query string</returns>
        public string ToString()
        {
            finish();
            return sb.ToString();
        }
    }
}
