using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace TowerOfDaedalus_WebApp_Kafka
{
    public interface IKafkaConsumer
    {
        void Subscribe(string topic);
        void Cancel();
        Task Run();
        void Stop();
    }

    public class KafkaConsumer : IKafkaConsumer
    {
        private static ILogger<KafkaConsumer> _logger;
        private ConsumerConfig _config;
        private IConsumer<Ignore, string> _consumer;
        private CancellationTokenSource _tokenSource;
        private bool _shouldStop = false;

        public KafkaOptions Options { get; }

        public KafkaConsumer(ILogger<KafkaConsumer> logger, IOptions<KafkaOptions> options) 
        {
            _logger = logger;
            Options = options.Value;
            _config = new ConsumerConfig
            {
                GroupId = Options.ConsumerGroup,
                BootstrapServers = Options.BrokerHost,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            _tokenSource = new CancellationTokenSource();
            _shouldStop = false;
        }

        ~KafkaConsumer()
        {
            _consumer.Close();
            _consumer.Dispose();
        }

        public void Subscribe(string topic)
        {
            _consumer.Subscribe(topic);
        }

        public void Cancel()
        {
            _tokenSource.Cancel();
        }

        public async Task Run()
        {
            await Task.Yield();
            try
            {
                while (!_shouldStop)
                {
                    try
                    {
                        var consumeReport = _consumer.Consume(_tokenSource.Token);
                        _logger.LogInformation($"Consumed message '{consumeReport.Message.Value}' at: '{consumeReport.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError($"Error occured: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }
        }

        public void Stop()
        {
            _shouldStop = true;
        }

    }
}
