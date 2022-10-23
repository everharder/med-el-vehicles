using MedEl.Vehicles.CLI.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.CLI.Commands
{
    internal interface ICommand
    {
        string Execute(CliInput input);
    }
}
