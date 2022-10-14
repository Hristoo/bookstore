using BookStore.BL.Kafka;
using CacheAPI.Models;
using BookStore.Models.Models.Configurations;
using CacheAPI.Services;
using CacheAPI.Repositories;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace CacheAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHostedService<Consumer<int, Book>>();
            builder.Services.AddHostedService<HostedService>();

            builder.Services.AddSingleton<Producer<int, Book>>();
            builder.Services.AddSingleton<HostedService>();
            builder.Services.AddSingleton<Repository<int, Book>>();


            // kafka settings
            builder.Services.Configure<KafkaSettings>(
                builder.Configuration.GetSection(nameof(KafkaSettings)));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}