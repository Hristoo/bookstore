using BookStode.DL.interfaces;
using BookStode.DL.Interfaces;
using BookStode.DL.Repositories.InMemoryRepositories;
using BookStode.DL.Repositories.MongoRepositories;
using BookStode.DL.Repositories.MsSql;
using BookStore.BL;
using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.BL.Services;
using BookStore.Caches;
using BookStore.Models.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace BookStore2.Extentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPersonRepository, PersonRepository>();
            services.AddSingleton<IAuthorRepository, AuthorSqlRepository>();
            services.AddSingleton<IBookRepository, BookSqlRepository>();
            services.AddSingleton<IEmployeesRepository, EmployeeSqlRepository>();
            services.AddSingleton<IUserInfoRepository, UserInfoSqlRepository>();
            services.AddSingleton<IShopingCartRepository, ShopingCartRepository>();

            return services;
        }

         public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IAuthorService, AuthorService>();
            services.AddSingleton<IBookService, BookService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            //services.AddSingleton<IUserInfoService, EmployeeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton<Producer<int, int>>();
            services.AddSingleton<Consumer<int, Book>>();
            services.AddSingleton<IPurchaseRepository, PurchaseRepository>();
            services.AddSingleton<Subcribe2Cache<int, Book>>();
            services.AddHostedService<DataflowService<Guid, Purchase>>();
            services.AddSingleton<IShopingCartService, ShopingCartService>();

            services.AddHttpClient(Options.DefaultName, c =>
            {
                // ...
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback =
                        (httpRequestMessage, cert, certChain, policyErrors) => true
                };
            });


            return services;
        }

    }
}
