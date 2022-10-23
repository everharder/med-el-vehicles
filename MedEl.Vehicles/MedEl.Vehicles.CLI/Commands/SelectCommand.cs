using MedEl.Vehicles.CLI.Input;
using MedEl.Vehicles.Common.DAC;
using MedEl.Vehicles.Common.Identification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.CLI.Commands
{
    internal class SelectCommand : BaseCommand
    {
        private readonly Context context;

        public SelectCommand(IServiceProvider services, Context context) : base(services)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override string Execute(CliInput input)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(input.Id))
            {
                IIdentification? element = getDac(input)
                    .Find(input.Id);

                if(element == null)
                {
                    sb.AppendLine($"Nothing found for id {input.Id}");
                }
                else
                {
                    context.SelectedElement = element;
                }
            }
            else 
            {
                if (context.SelectedElement != null)
                {
                    sb.AppendLine($"Currently selected: {context.SelectedElement}");
                }
                else
                {
                    sb.AppendLine($"Nothing selected.");
                }
            }

            return sb.ToString();
        }
    }
}
