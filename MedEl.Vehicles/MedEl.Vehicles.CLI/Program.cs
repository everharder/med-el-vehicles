using CommandLine;
using MedEl.Vehicles.CLI;
using MedEl.Vehicles.CLI.Commands;
using MedEl.Vehicles.CLI.Input;
using MedEl.Vehicles.Common;
using MedEl.Vehicles.Common.DAC;
using MedEl.Vehicles.Common.Interfaces;
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

// SETUP services and config
IServiceProvider services = setupServices();
IRepository repository = setupDataModel(services);

Parser parser = setupParser();
CommandFactory commandFactory = services.GetRequiredService<CommandFactory>();

bool running = true;
while (running)
{
    try
    {
        Console.Write("Input: ");
        string? i = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(i))
        {
            continue;
        }
        parser.ParseArguments<CliInput>(i.Split(' '))
            .WithParsed(input =>
            {
                if(input.Command == ECommand.Exit)
                {
                    Console.WriteLine("Goodbye!");
                    running = false;
                    return;
                }

                // create commands
                List<ICommand> commands = commandFactory.CreateCommands(input);

                // execute commands
                string output = string.Join(Environment.NewLine, commands.Select(x => x.Execute(input)));
                Console.WriteLine(output);
            });
    }
    catch (Exception e)
    {
        Console.Error.WriteLine(e);
    }
}



IServiceProvider setupServices()
{
    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureLogging((logging) => logging
            .AddConsole())
        .ConfigureHostConfiguration((configBuilder) => configBuilder.AddJsonFile("appsettings.json"))
        .ConfigureServices((_, services) => services
            .AddCli())
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

static Parser setupParser()
{
    return new Parser((config) =>
    {
        config.CaseSensitive = false;
        config.AutoHelp = true;
        config.CaseInsensitiveEnumValues = true;
        config.HelpWriter = Console.Error;
    });
}