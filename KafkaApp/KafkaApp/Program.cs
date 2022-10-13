using System.Collections.Generic;
using Confluent.Kafka;
using MessagePack;

namespace KafkaProducer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092",
            };

            var producer = new ProducerBuilder<int, byte[]>(config).Build();

            var person = new Person()
            {
                Id = 1,
                Name = "Canko",
                Age = 89
            };
            try
            {
                var msg = new Message<int, byte[]>()
                {
                    Key = person.Id,
                    Value = MessagePackSerializer.Serialize(person)
                };

                var result = await producer.ProduceAsync("personTopic", msg);

                if (result != null)
                {
                    Console.WriteLine($"Delivered: {result.Value} to {result.TopicPartitionOffset}");
                }

            }
            catch (ProduceException<int, Person> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }

}