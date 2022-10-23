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
    internal class CliInput
    {
        [Option(shortName: 'c', longName: "command", Required = true, HelpText = $"Selects the command (Create, List, Delete, Select, Print, Exit)")]
        public ECommand Command { get; set; }

        [Option(shortName: 't', longName: "type", Required = false, HelpText = "The type of entity to access (Car, Motorcycle, Manufacturer).")]
        public EEntityTypeName? Type { get; set; }

        [Option(shortName: 'i', longName: "id", Required = false, HelpText = "The id of the element to select as context (for certain commands like print)")]
        public string? Id { get; set; }
    }
}
