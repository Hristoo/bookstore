using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.Background
{
    public class MyBackroundService : IHostedService
    {
        private readonly ILogger<MyBackroundService> _logger;
        public static Timer t = new Timer(DoWork, null, 0, 1000);

        public static void DoWork(object? state)
        {
            Console.WriteLine(DateTime.Now);
        }

        public MyBackroundService(ILogger<MyBackroundService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Hello from {nameof(StartAsync)}");

            Console.WriteLine("hd");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Hello from {nameof(StartAsync)}");

            Console.WriteLine("hd_enddd");

            return Task.CompletedTask;
        }

        //protected override Task ExecuteAsync(CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Hello from {}");

        //    return Task.CompletedTask;
        //}

    }
}
