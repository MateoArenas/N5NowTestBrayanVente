using MediatR;
using Microsoft.EntityFrameworkCore;
using N5NowTestBrayanVente.Infrastructure.Contexts;
using N5NowTestBrayanVente.Infrastructure.Remotes;
using N5NowTestBrayanVente.Infrastructure.Repositories;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureLogs();
builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

string connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
builder.Services.AddDbContext<N5NowTestDBContext>(x => x.UseSqlServer(connectionString));


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


#region helper
void ConfigureLogs() 
{
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureELS(configuration, env))
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureELS(IConfigurationRoot configuration, string env)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ELKConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{env.ToLower().Replace(".","-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}
#endregion