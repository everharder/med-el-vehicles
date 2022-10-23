using MedEl.Vehicles.CLI.Input;
using MedEl.Vehicles.Common.DAC;
using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.CLI.Commands
{
    internal class ListCommand : BaseCommand
    {
        public ListCommand(IServiceProvider services) : base(services)
        {
        }

        public override string Execute(CliInput input)
        {
            IDAC dac = getDac(input);
            List<IIdentification> elements = dac.FindAll();
            StringBuilder sb = new StringBuilder();

            if (elements.Count == 0)
            {
                sb.AppendLine("Nothing to display...");
            }
            else if (elements.First() is ITablePrintable table)
            {
                sb.AppendLine(table.ToTableHeader());
                sb.AppendLine(string.Join("=", Enumerable.Range(0, table.ToTableHeader().Length).Select(x => string.Empty)));
                foreach (ITablePrintable e in elements.Cast<ITablePrintable>())
                {
                    sb.AppendLine(e.ToTableRow());
                }
            }
            else
            {
                elements.ForEach(x => sb.AppendLine(x.ToString()));
            }

            return sb.ToString();
        }
    }
}
