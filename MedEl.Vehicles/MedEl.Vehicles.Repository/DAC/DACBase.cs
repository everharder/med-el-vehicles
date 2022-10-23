using MedEl.Vehicles.Common.DAC;
using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.DAC
{
    public class DACBase<TEntity> : ITypedDAC<TEntity> where TEntity : IIdentification
    {
        private readonly IRepository repository;

        public DACBase(IRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool Delete(TEntity entity)
            => repository.Delete(entity);

        public bool Delete(IIdentification entity)
            => Delete((TEntity)entity);

        public TEntity? Find(string id)
            => repository.Get<TEntity>(id);

        public List<TEntity> FindAll()
            => repository.GetAll<TEntity>();

        public void Save(TEntity entity)
            => repository.Save(entity);

        public void Save(IIdentification entity)
            => Save((TEntity)entity);

        IIdentification? IDAC.Find(string id)
            => Find(id);

        List<IIdentification> IDAC.FindAll()
            => FindAll().Cast<IIdentification>().ToList();
    }
}
