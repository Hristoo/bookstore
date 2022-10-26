using System.Threading.Tasks.Dataflow;
using Confluent.Kafka;
using MessagePack;

namespace DataflowEx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var consumer = new Consumer<Ignore, byte[]>();

            var transformBlock = new TransformBlock<byte[], Purchase>(result =>
            {        
                var r = MessagePackSerializer.Deserialize<Purchase>(result);
                return r;
            });

            var actionBlock = new ActionBlock<Purchase>(value =>
            {
                Console.WriteLine($"{value}");
            });

            transformBlock.LinkTo(actionBlock);

            while (consumer != null)
            {
                transformBlock.Post(consumer.GetConsumer());
            }
                transformBlock.Completion.Wait();
        }
    }
}