using MedEl.Vehicles.CLI.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.CLI.Commands
{
    internal class CommandFactory
    {
        private readonly IServiceProvider services;

        public CommandFactory(IServiceProvider services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public List<ICommand> CreateCommands(CliInput input)
        {
            List<ICommand> commands = new List<ICommand>();
            if (input.Command == ECommand.Select || !string.IsNullOrWhiteSpace(input.Id))
            {
                commands.Add(services.GetRequiredService<SelectCommand>());
            }

            if (input.Command != ECommand.Select)
            {
                switch (input.Command)
                {
                    case ECommand.List:
                        commands.Add(services.GetRequiredService<ListCommand>());
                        break;
                    case ECommand.Print:
                        commands.Add(services.GetRequiredService<PrintCommand>());
                        break;
                    case ECommand.Create:
                        commands.Add(services.GetRequiredService<CreateCommand>());
                        break;
                    case ECommand.Delete:
                        commands.Add(services.GetRequiredService<DeleteCommand>());
                        break;
                    default:
                        throw new NotSupportedException(input.Command.ToString());
                }
            }
            return commands;
        }
    }
}
