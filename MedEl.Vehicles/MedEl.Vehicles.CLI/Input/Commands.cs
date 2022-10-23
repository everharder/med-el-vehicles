using CommandLine;
using CommandLine.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.CLI.Input
{
    internal class Commands
    {

        [Option(Required = false, HelpText = "The type of entity to access (e.g. Car, Motorcycle, Manufacturer).")]
        public EEntityTypeName? Type { get; set; }

        [Option(Required = true, HelpText = $"Selects the command (Create, List, Delete, Select, Print, Exit)")]
        public ECommand Command { get; set; }
    }
}
