using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TowerOfDaedalus_WebApp_Kafka
{
    /// <summary>
    /// Interfance for defining arango utility classes
    /// </summary>
    public interface IKafkaUtils
    {
    }

    /// <summary>
    /// Class for establishing and maintaining an arango database
    /// </summary>
    public class Utilities : IKafkaUtils
    {
        private static ILogger<Utilities> _logger;


        /// <summary>
        /// Default constructor for arango utilities
        /// </summary>
        /// <param name="logger">logging provider for logging messages</param>
        public Utilities(ILogger<Utilities> logger)
        {
            _logger = logger;
        }

        
    }
}

