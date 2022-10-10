using BookStode.DL.interfaces;
using BookStode.DL.Interfaces;
using BookStode.DL.Repositories.InMemoryRepositories;
using BookStode.DL.Repositories.MsSql;
using BookStore.BL;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using Microsoft.AspNetCore.Identity;

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

            return services;
        }

    }
}
