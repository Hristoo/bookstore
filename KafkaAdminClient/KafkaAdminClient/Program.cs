using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace KafkaAdminClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = new AdminClientConfig()
            {
                BootstrapServers = "localhost:9092"
            };

            using (var adminClient = new AdminClientBuilder(config).Build())
            {
                try
                {
                    //await adminClient.CreateTopicsAsync(new List<TopicSpecification>()
                    //{
                    //    new TopicSpecification()
                    //    {
                    //        Name = "TopicFromAdminClient",
                    //        NumPartitions = 1,
                    //        ReplicationFactor = 1
                    //    }
                    //});

                    var result = adminClient.GetMetadata("Person", TimeSpan.FromSeconds(1000));
                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e); ;
                }
            };
        }
    }
}