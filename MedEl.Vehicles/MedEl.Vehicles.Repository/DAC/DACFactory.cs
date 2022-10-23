using MedEl.Vehicles.Common.DAC;
using MedEl.Vehicles.Common.Identification;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MedEl.Vehicles.Repository.DAC
{
    internal class DACFactory : IDACFactory
    {
        private readonly Dictionary<Type, Type> dacTypeCache = new Dictionary<Type, Type>();
        private readonly IServiceProvider serviceProvider;

        public DACFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IDAC CreateDAC<TEntity>() where TEntity : IIdentification
            => CreateDAC(typeof(TEntity));

        public IDAC CreateDAC(Type entityType)
        {
            if (!typeof(IIdentification).IsAssignableFrom(entityType))
            {
                throw new ArgumentException($"EntityType {entityType.Name} is not derived from {nameof(IIdentification)}");
            }

            if (!dacTypeCache.TryGetValue(entityType, out Type? dacType))
            {
                Type generic = typeof(ITypedDAC<>);
                Type[] typeArgs = { entityType };
                dacType = generic.MakeGenericType(typeArgs);
                dacTypeCache[entityType] = dacType;
            }

            object? dac = this.serviceProvider.GetService(dacType);
            if (dac == null)
            {
                throw new Exception($"No dac registered for entity type {entityType.Name}");
            }
            return (IDAC)dac;
        }
    }
}
