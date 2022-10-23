using MedEl.Vehicles.CLI.Input;
using MedEl.Vehicles.Common.DAC;
using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.CLI.Commands
{
    internal class PrintCommand : BaseCommand
    {
        private readonly Context context;

        public PrintCommand(IServiceProvider services, Context context) : base(services)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override string Execute(CliInput input)
        {
            StringBuilder sb = new StringBuilder();
            if(context.SelectedElement is IPrettyPrintable prettyPrintable)
            {
                sb.AppendLine(prettyPrintable.ToPrettyString());
            }
            else if (context.SelectedElement != null)
            {
                sb.AppendLine(context.SelectedElement.ToString());
            }
            return sb.ToString();
        }
    }
}
