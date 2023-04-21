namespace TowerOfDaedalus_WebApp_Kafka
{
    public class KafkaOptions
    {
        public string ConsumerGroup { get; set; }
        public string BrokerHost { get; set; }

        public List<string> Topics { get; set; }
    }
}
