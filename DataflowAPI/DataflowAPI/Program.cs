using DataflowAPI.Kafka;
using DataflowAPI.Models;
using DataflowAPI.Models.Configuration;
using DataflowAPI.Services;

namespace DataflowAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // kafka settings
            builder.Services.Configure<KafkaSettings>(
                builder.Configuration.GetSection(nameof(KafkaSettings)));

            // Add services to the container.
            builder.Services.AddSingleton<Producer<Guid, Purchase>>();
            //builder.Services.AddSingleton<GeneratePurchaseService>();
            builder.Services.AddHostedService<GeneratePurchaseService>();

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