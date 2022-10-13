using Confluent.Kafka;
using MessagePack;

namespace KafkaConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = "123"
            };

            var consumer = new ConsumerBuilder<int, Person>(config)
                .SetValueDeserializer(new CustomPackDeserialize<Person>())
                .Build();

            consumer.Subscribe("personTopic");

            while (true)
            {
                var cr = consumer.Consume();

                Console.WriteLine($"Receved person with id:{cr.Message.Value.Id} name:{cr.Message.Value.Name}");
            }
        }
    }
}