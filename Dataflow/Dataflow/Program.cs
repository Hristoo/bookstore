using System.Threading.Tasks.Dataflow;

namespace Dataflow
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var batchedJoinBlock = new BatchedJoinBlock<int, int>(3);

            var printToConsole = new ActionBlock<Tuple<IList<int>, IList<int>>>(result =>
            {
                foreach (var r in result.Item1)
                {
                    Console.WriteLine($"from target1: {r}");
                }
                foreach (var r in result.Item2)
                {
                    Console.WriteLine($"from target2: {r}");
                }
            });

            Console.WriteLine("-------------");

            batchedJoinBlock.LinkTo(printToConsole);

            batchedJoinBlock.Target1.Post(1);
            batchedJoinBlock.Target1.Post(2);
            batchedJoinBlock.Target2.Post(3);
            batchedJoinBlock.Target2.Post(4);
            batchedJoinBlock.Target2.Post(5);
            batchedJoinBlock.Target1.Post(6);

            batchedJoinBlock.Completion.Wait(1000);


            //var produser1 = new BufferBlock<int>();
            //var produser2 = new BufferBlock<int>();
            //var sinkActionBlock = new ActionBlock<int>(v =>
            //{
            //    Console.WriteLine($"Action block consume value {v}");
            //});

            //var joinBlock = new JoinBlock<int, int>(new GroupingDataflowBlockOptions()
            //{
            //    Greedy = false,
            //});
            //var printToConsoleBlock = new ActionBlock<Tuple<int, int>>(value =>
            //{
            //    Console.WriteLine($"Received value from join block {value.Item1}, {value.Item2}");
            //});

            //produser1.LinkTo(joinBlock.Target1);
            //produser2.LinkTo(joinBlock.Target2);
            //joinBlock.LinkTo(printToConsoleBlock);

            //produser1.Post(1);
            //produser1.Post(1);
            //produser2.Post(2);

            //await Task.Delay(300);

            //produser1.LinkTo(sinkActionBlock);
            //produser2.LinkTo(sinkActionBlock);

            //produser2.Post(2);

            //Console.ReadLine();

            //var produser1 = new BufferBlock<int>();
            //var produser2 = new BufferBlock<int>();

            //var joinBlock = new JoinBlock<int, int>(new GroupingDataflowBlockOptions()
            //{
            //    Greedy = false,
            //});

            //var actionBlock = new ActionBlock<Tuple<int, int>>(v =>
            //{
            //    var (item1, item2) = v;
            //    Console.WriteLine($"{item1},{item2}");
            //});

            //produser1.LinkTo(joinBlock.Target1);
            //produser2.LinkTo(joinBlock.Target2);

            //joinBlock.LinkTo(actionBlock);

            //produser1.Post(1);
            //produser1.Post(1);
            //produser2.Post(2);

            //joinBlock.Completion.Wait();

            //var produser1 = new BufferBlock<int>();
            //var produser2 = new BufferBlock<int>();

            //var batchBlock = new BatchBlock<int>(2, new GroupingDataflowBlockOptions()
            //{
            //    Greedy = true,
            //});

            //var actionBlock = new ActionBlock<IEnumerable<int>>(values =>
            //{
            //    Console.WriteLine($"The average value is {values.Average()}");
            //});

            //produser1.LinkTo(batchBlock);
            //produser2.LinkTo(batchBlock);

            //batchBlock.LinkTo(actionBlock);

            //produser1.Post(10);
            //produser1.Post(10);
            //await Task.Delay(10);

            //produser2.Post(20);
            //produser2.Post(20);

            //produser1.Completion.Wait();


            //var bufferBlock = new BufferBlock<int>();

            //ITargetBlock<int> producer = bufferBlock;
            //BufferBlock<int> consumer = bufferBlock;

            //Task.Run(async () =>
            //{
            //    producer.Post(1);
            //    producer.Post(2);

            //    await Task.Delay(5000);

            //    producer.Post(3);
            //    producer.Post(4);
            //    producer.Complete();
            //});

            //var consumerTask = Task.Run(async () =>
            //{
            //    while (await consumer.OutputAvailableAsync())
            //    {
            //        var data = consumer.Receive();
            //        Console.WriteLine($"Received:{data}");

            //    }
            //});

            //consumerTask.Wait();
        }

        private static async Task<Response> ProcessRequest(Request request)
        {
            await Task.Delay(1000);

            if (request.RequestType.Equals("type3"))
            {
                throw new InvalidOperationException("test");
            }
            return new Response()
            {
                HasError = false,
                RequestType = request.RequestType,
            };
        }

        private static async Task<bool> ComplicatedComputation(int reqest)
        {
            await Task.Delay(2000);
            return reqest < 3;
        }
    }
}