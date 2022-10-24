using MedEl.Vehicles.CLI.Input;
using MedEl.Vehicles.Common.DAC;
using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Common.Interfaces;
using MedEl.Vehicles.Logic.TireChange;
using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
using MedEl.Vehicles.Model.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.CLI.Commands
{
    internal class ChangeTiresCommand : BaseCommand
    {
        private readonly Context context;
        private readonly ITireChangeService tireChangeService;
        private readonly IDACFactory dACFactory;
        private readonly ITireFactory tireFactory;

        public ChangeTiresCommand(IServiceProvider services, 
            Context context, 
            ITireChangeService tireChangeService, 
            IDACFactory dACFactory,
            ITireFactory tireFactory) : base(services)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.tireChangeService = tireChangeService ?? throw new ArgumentNullException(nameof(tireChangeService));
            this.dACFactory = dACFactory ?? throw new ArgumentNullException(nameof(dACFactory));
            this.tireFactory = tireFactory ?? throw new ArgumentNullException(nameof(tireFactory));
        }

        public override string Execute(CliInput input)
        {
            StringBuilder sb = new StringBuilder();
            if(context.SelectedElement == null)
            {
                sb.AppendLine("No element selected!");
                return sb.ToString();
            }

            if(context.SelectedElement is not IVehicle vehicle)
            {
                sb.Append("Changing tires is only supported for vehicles!");
                return sb.ToString();
            }

            // determine what kind of tires are currently mounted
            ETireType currentTireType = vehicle.Chassis.Axles.SelectMany(x => x.Tires).First().Type;

            // create tires -> switch tire type 
            IEnumerable<ITire> tires;
            if(currentTireType == ETireType.WinterTire)
            {
                tires = tireFactory.CreateSummerTires(vehicle);
                sb.AppendLine("Switching to summer tires.");
            }
            else if (currentTireType == ETireType.SummerTire)
            {
                tires = tireFactory.CreateWinterTires(vehicle);
                sb.AppendLine("Switching to winter tires.");
            }
            else
            {
                throw new NotImplementedException(currentTireType.ToString());
            }

            tireChangeService.ChangeTires(vehicle, tires);

            dACFactory.CreateDAC(vehicle.GetType()).Save(vehicle);

            sb.AppendLine("Tires changed!");

            return sb.ToString();
        }
    }
}
