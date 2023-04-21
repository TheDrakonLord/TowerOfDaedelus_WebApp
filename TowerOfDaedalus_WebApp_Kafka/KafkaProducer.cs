using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace TowerOfDaedalus_WebApp_Kafka
{
    public interface IKafkaProducer
    {
        Task produceMessageAsync(string topic, string message);
    }

    public class KafkaProducer : IKafkaProducer
    {
        private static ILogger<KafkaProducer> _logger;
        private ProducerConfig _config;
        private Action<DeliveryReport<Null, string>> _handler;
        private IProducer<Null, string> _producer;

        public KafkaOptions Options { get; }

        public KafkaProducer(ILogger<KafkaProducer> logger, IOptions<KafkaOptions> options) 
        {
            _logger = logger;
            Options = options.Value;
            _config = new ProducerConfig { BootstrapServers = Options.BrokerHost };
            _handler = r =>
                _logger.Log(!r.Error.IsError
                    ? LogLevel.Information
                    : LogLevel.Error,
                !r.Error.IsError
                    ? $"Delivered message to {r.TopicPartitionOffset}"
                    : $"Delivery Error: {r.Error.Reason}");

            _producer = new ProducerBuilder<Null, string>(_config).Build();
        }

        ~KafkaProducer() 
        {
            _producer.Dispose();
        }

        public async Task produceMessageAsync(string topic, string message)
        {
            try
            {
                Message<Null, string> _message = new() { Value = message };
                var deliveryReport = await _producer.ProduceAsync(topic, _message);
                _logger.LogInformation($"Delivered '{deliveryReport.Value}' to '{deliveryReport.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                _logger.LogError($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
