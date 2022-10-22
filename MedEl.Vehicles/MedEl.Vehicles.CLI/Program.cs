using MedEl.Vehicles.Common;
using MedEl.Vehicles.Common.Repository;
using MedEl.Vehicles.Model;
using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
using MedEl.Vehicles.Model.Factory;
using MedEl.Vehicles.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

// SETUP services and config
IServiceProvider services = setupServices();
IRepository repository = setupDataModel(services);

ILogger logger = services.GetRequiredService<ILogger>();
IVehicleFactory vehicleFactory = services.GetRequiredService<IVehicleFactory>();

IManufacturer manufacturerToyota = repository.Get<IManufacturer>("Toyota")!;
IVehicle toyota = vehicleFactory.CreateCar(manufacturerToyota);
//toyota.Move();
logger.LogInformation(toyota.ToPrettyString());

IManufacturer manufacturerHonda = repository.Get<IManufacturer>("Honda")!;
IVehicle honda = vehicleFactory.CreateMotorcycle(manufacturerHonda);
logger.LogInformation(honda.ToPrettyString());
//honda.Move();

Console.Read();




IServiceProvider setupServices()
{
    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureLogging((logging) => logging
            .AddConsole())
        .ConfigureHostConfiguration((configBuilder) => configBuilder.AddJsonFile("appsettings.json"))
        .ConfigureServices((_, services) => services
            .AddCommon()
            .AddModel()
            .AddRepository())
        .Build();

    return host.Services;
}

IRepository setupDataModel(IServiceProvider services)
{
    IManufacturerFactory manufacturerFactory = services.GetRequiredService<IManufacturerFactory>();
    IRepository repository = services.GetRequiredService<IRepository>();

    IManufacturer honda = manufacturerFactory
        .CreateManufacturer("Honda", EVehicleType.Motorcycle | EVehicleType.Car);
    repository.Save(honda);

    IManufacturer toyota = manufacturerFactory
        .CreateManufacturer("Toyota", EVehicleType.Car);
    repository.Save(toyota);

    IManufacturer ktm = manufacturerFactory
        .CreateManufacturer("KTM", EVehicleType.Motorcycle);
    repository.Save(ktm);

    return repository;
}