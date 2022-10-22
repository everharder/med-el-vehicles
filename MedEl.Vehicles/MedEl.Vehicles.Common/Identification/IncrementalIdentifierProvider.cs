using MedEl.Vehicles.Common.Interfaces;
using MedEl.Vehicles.Common.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Common.Identification
{
    internal class IncrementalIdentifierProvider : IIdentificationProvider
    {
        private readonly ConcurrentDictionary<Type, IdContainer> _containers = new ConcurrentDictionary<Type, IdContainer>();
        private readonly IRepository? repository;

        public IncrementalIdentifierProvider(IServiceProvider services)
        {
            this.repository = services.GetService<IRepository>();
        }

        public string GetId<TIdentification>() where TIdentification : IIdentification
        {
            IdContainer container = _containers.GetOrAdd(typeof(TIdentification), (t) => getOrCreateIdContainer<TIdentification>());
            return container.GetId().ToString();
        }

        private IdContainer getOrCreateIdContainer<TIdentification>() where TIdentification : IIdentification
        {
            // if repository is registered retrieve the latest id from the repo
            // otherwise start from 0
            string? highestStringId = repository?.GetHighestId<TIdentification>();
            if (!long.TryParse(highestStringId, out long highestId))
            {
                highestId = -1;
            }

            return new IdContainer(typeof(TIdentification), highestId);
        }

        private class IdContainer
        {
            private long lastId;

            public IdContainer(Type identificationType, long lastId)
            {
                IdentificationType = identificationType;
                this.lastId = lastId;
            }

            public Type IdentificationType { get; }

            public long GetId()
            {
                return Interlocked.Increment(ref lastId);
            }
        }
    }
}
