using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Confluent.Kafka.Admin;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace TowerOfDaedalus_WebApp_Kafka
{
    /// <summary>
    /// Interfance for defining arango utility classes
    /// </summary>
    public interface IKafkaUtils
    {
        /// <summary>
        /// 
        /// </summary>
        void CreateTopics();
    }

    /// <summary>
    /// Class for establishing and maintaining an arango database
    /// </summary>
    public class Utilities : IKafkaUtils
    {
        private static ILogger<Utilities> _logger;

        public KafkaOptions Options { get; }

        /// <summary>
        /// Default constructor for arango utilities
        /// </summary>
        /// <param name="logger">logging provider for logging messages</param>
        public Utilities(ILogger<Utilities> logger, IOptions<KafkaOptions> options)
        {
            _logger = logger;
            Options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateTopics()
        {
            using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = Options.BrokerHost }).Build())
            {
                var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                var topicsMetadata = metadata.Topics;
                var topicNames = metadata.Topics.Select(a => a.Topic).ToList();
                foreach (var topic in Options.Topics)
                {
                    if (!topicNames.Contains(topic))
                    {
                        _logger.LogInformation("CreateTopicAsync || creating topic [{topic}] on brokers [{broker}]", topic, Options.BrokerHost);
                        try
                        {
                            Task.Run(() => adminClient.CreateTopicsAsync(new TopicSpecification[] {
                                new TopicSpecification { Name = topic, ReplicationFactor = 1, NumPartitions = 1 } }));
                        }
                        catch (CreateTopicsException e)
                        {
                            Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("CreateTopicAsync || creating topic [{topic}] already exists on broker [{broker}]", topic, Options.BrokerHost);
                    }
                }
            }
        }
    }
}

