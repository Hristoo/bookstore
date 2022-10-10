using BookStore2.Extentions;
using FluentValidation.AspNetCore;
using FluentValidation;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using BookStore2.HealthChecks;
using MediatR;
using BookStore.BL.CommandHandlers;
using BookStore2.MiddleWare;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookStore.Models.Models.Users;
using BookStode.DL.Repositories.MsSql;
using BookStore.Models.Models;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
//serilog
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.RegisterRepositories()
    .RegisterServices()
    .AddAutoMapper(typeof(Program));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JMT",
        Name = "JMT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = " Put **_ONLY_** you JWT Bearer token in the text box below",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    x.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecurityScheme, Array.Empty<string>() }
    });
});


builder.Services.AddAuthorization(options =>
{
options.AddPolicy("Admin", policy => { policy.RequireClaim("Admin"); });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

//health checks
builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("SQL Server")
    .AddCheck<CustomExeption>("Custom Check")
    .AddUrlGroup(new Uri("https://google.com"), name: "Google Service");

builder.Services.AddMediatR(typeof(GetAllBooksHandler).Assembly);

builder.Services.AddIdentity<UserInfo, UserRole>().AddUserStore<UserInfoStore>().AddRoleStore<UserRoleStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.RegisterHealthChecks();

app.UseMiddleware<ErrorHandlingMiddleware>();

//app.UseMiddleware<CustomMiddlewareErrorHandler>();

app.Run();
