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
    internal class DeleteCommand : BaseCommand
    {
        private readonly Context context;

        public DeleteCommand(IServiceProvider services, Context context) : base(services)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override string Execute(CliInput input)
        {
            StringBuilder sb = new StringBuilder();
            if(context.SelectedElement == null)
            {
                sb.AppendLine("Nothing to delete...");
                return sb.ToString();
            }

            IDAC dac = getDac(context.SelectedElement.GetType());
            bool success = dac.Delete(context.SelectedElement!);

            if(success)
            {
                context.SelectedElement = null;
                sb.AppendLine("Element deleted successfully");
            }
            else
            {
                sb.AppendLine("Found nothing to delete.");
            }

            return sb.ToString();
        }
    }
}
