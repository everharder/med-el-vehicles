using MedEl.Vehicles.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration((configBuilder) => configBuilder.AddJsonFile("appsettings.json"))
    .ConfigureServices((_, services) =>
        services.AddRepository())
    .Build();

RepositoryConfiguration config = host.Services.GetRequiredService<RepositoryConfiguration>();
Console.WriteLine(config.UseCaching);

Console.Read();
