using BookStore.BL.Kafka;
using BookStore.Models.Models.Configurations;
using CacheAPI.Models;
using CacheAPI.Repositories;
using CacheAPI.Settings;
using Microsoft.Extensions.Options;

namespace CacheAPI.Services
{
    public class HostedService : IHostedService
    {
        private readonly Repository<int, Book> _repo;
        private readonly Producer<int, Book> _producer;
        private readonly IOptionsMonitor<CacheSettings> _settings;

        public HostedService(Repository<int, Book> repo, Producer<int, Book> producer, IOptionsMonitor<CacheSettings> settings)
        {
            _repo = repo;
            _producer = producer;
            _settings = settings;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            DateTime date = DateTime.Now.AddDays(-100);

            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    
                    await Task.Delay(_settings.CurrentValue.Interval, cancellationToken);

                    var result = await _repo.GetDBInfo(_settings.CurrentValue.storedProcedure, date);

                    foreach (var item in result)
                    {
                        if (item.LastUpdated > date) 
                        { 
                            date = item.LastUpdated; 
                        }

                        await _producer.Produce(item.Id, item);
                    }
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
