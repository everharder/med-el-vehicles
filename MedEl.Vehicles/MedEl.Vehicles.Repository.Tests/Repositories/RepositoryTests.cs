using MedEl.Vehicles.Common.Configuration;
using MedEl.Vehicles.Repository.FileSystem;
using MedEl.Vehicles.Repository.InMemory;
using MedEl.Vehicles.Repository.PseudoRepositories;
using MedEl.Vehicles.Repository.Tests.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.Tests.Repositories
{
    public class RepositoryTests : TestBase
    {
        protected override IServiceCollection createServiceCollection()
        {
            IServiceCollection serviceCollection = base.createServiceCollection();

            // override configuration for this test class
            SimpleConfiguration configuration = new SimpleConfiguration();
            string repositoryDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            configuration["Repository:FileSystemRepositoryPath"] = repositoryDir;
            configuration["Repository:UseCaching"] = "false";
            serviceCollection.AddSingleton<IConfigurationDictionary>(configuration);

            serviceCollection.AddSingleton<CachedRepository>(provider => 
                (CachedRepository)provider.GetRequiredService<CachedRepositoryFactory>()
                    .CreateInstance(provider.GetRequiredService<FileSystemRepository>()));

            return serviceCollection;
        }

        [Theory]
        [InlineData(typeof(InMemoryRepository))]
        [InlineData(typeof(FileSystemRepository))]
        [InlineData(typeof(CachedRepository))]
        public void GetAll_Empty_EmptyList(Type repositoryType)
        {
            IServiceProvider services = createServiceProvider();
            IRepository repository = (IRepository)services.GetRequiredService(repositoryType);

            try
            {
                List<Persistable> lst = repository.GetAll<Persistable>();
                Assert.NotNull(lst);
                Assert.Empty(lst);
            }
            finally
            {
                repository.Truncate();
            }
        }

        [Theory]
        [InlineData(typeof(InMemoryRepository))]
        [InlineData(typeof(FileSystemRepository))]
        [InlineData(typeof(CachedRepository))]
        public void GetAll_NotEmpty_AllRetrieved(Type repositoryType)
        {
            IServiceProvider services = createServiceProvider();
            IRepository repository = (IRepository)services.GetRequiredService(repositoryType);

            try
            {
                // create some elements and save them
                List<Persistable> p1 = Enumerable.Range(0, 10).Select(x => new Persistable("p1_" + x)).ToList();
                p1.ForEach(x => repository.Save(x));

                List<OtherPersistable> p2 = Enumerable.Range(0, 3).Select(x => new OtherPersistable("p2_" + x)).ToList();
                p2.ForEach(x => repository.Save(x));

                // retrieve again
                List<Persistable> lst1 = repository.GetAll<Persistable>();
                Assert.NotNull(lst1);
                Assert.NotEmpty(lst1);
                Assert.All(lst1, (p) => p1.Any(x => x.Id == p.Id));

                List<OtherPersistable> lst2 = repository.GetAll<OtherPersistable>();
                Assert.NotNull(lst2);
                Assert.NotEmpty(lst2);
                Assert.All(lst2, (p) => p2.Any(x => x.Id == p.Id));
            }
            finally
            {
                repository.Truncate();
            }
        }

        [Theory]
        [InlineData(typeof(InMemoryRepository))]
        [InlineData(typeof(FileSystemRepository))]
        [InlineData(typeof(CachedRepository))]
        public void Get_Existing_Retrieved(Type repositoryType)
        {
            IServiceProvider services = createServiceProvider();
            IRepository repository = (IRepository)services.GetRequiredService(repositoryType);

            try
            {
                // create some elements and save them
                List<Persistable> p1 = Enumerable.Range(0, 10).Select(x => new Persistable("p1_" + x)).ToList();
                p1.ForEach(x => repository.Save(x));

                // retrieve again
                Persistable? element = repository.Get<Persistable>("p1_3");
                Assert.NotNull(element);
                Assert.Equal(p1[3].Id, element?.Id);
            }
            finally
            {
                repository.Truncate();
            }
        }

        [Theory]
        [InlineData(typeof(InMemoryRepository))]
        [InlineData(typeof(FileSystemRepository))]
        [InlineData(typeof(CachedRepository))]
        public void Get_NonExisting_NotRetrieved(Type repositoryType)
        {
            IServiceProvider services = createServiceProvider();
            IRepository repository = (IRepository)services.GetRequiredService(repositoryType);

            try
            {
                // create some elements and save them
                List<Persistable> p1 = Enumerable.Range(0, 10).Select(x => new Persistable("p1_" + x)).ToList();
                p1.ForEach(x => repository.Save(x));

                // retrieve again
                Persistable? element = repository.Get<Persistable>(Guid.NewGuid().ToString());
                Assert.Null(element);
            }
            finally
            {
                repository.Truncate();
            }
        }

        [Theory]
        [InlineData(typeof(InMemoryRepository))]
        [InlineData(typeof(FileSystemRepository))]
        [InlineData(typeof(CachedRepository))]
        public void Delete_Existing_Deleted(Type repositoryType)
        {
            IServiceProvider services = createServiceProvider();
            IRepository repository = (IRepository)services.GetRequiredService(repositoryType);

            try
            {
                // create some elements and save them
                List<Persistable> p1 = Enumerable.Range(0, 10).Select(x => new Persistable("p1_" + x)).ToList();
                p1.ForEach(x => repository.Save(x));

                // retrieve again
                Persistable? element = repository.Get<Persistable>("p1_3");
                Assert.NotNull(element);

                // delete
                bool deleted = repository.Delete<Persistable>("p1_3");
                Assert.True(deleted);

                element = repository.Get<Persistable>("p1_3");
                Assert.Null(element);
            }
            finally
            {
                repository.Truncate();
            }
        }

        [Theory]
        [InlineData(typeof(InMemoryRepository))]
        [InlineData(typeof(FileSystemRepository))]
        [InlineData(typeof(CachedRepository))]
        public void Delete_NonExisting_NotDeleted(Type repositoryType)
        {
            IServiceProvider services = createServiceProvider();
            IRepository repository = (IRepository)services.GetRequiredService(repositoryType);

            try
            {
                // create some elements and save them
                List<Persistable> p1 = Enumerable.Range(0, 10).Select(x => new Persistable("p1_" + x)).ToList();
                p1.ForEach(x => repository.Save(x));

                // delete
                bool deleted = repository.Delete<Persistable>(Guid.NewGuid().ToString());
                Assert.False(deleted);
            }
            finally
            {
                repository.Truncate();
            }
        }

        [Theory]
        [InlineData(typeof(InMemoryRepository))]
        [InlineData(typeof(FileSystemRepository))]
        [InlineData(typeof(CachedRepository))]
        public void Update_Existing_Updated(Type repositoryType)
        {
            IServiceProvider services = createServiceProvider();
            IRepository repository = (IRepository)services.GetRequiredService(repositoryType);

            try
            {
                // create some elements and save them
                List<Persistable> p1 = Enumerable.Range(0, 10).Select(x => new Persistable("p1_" + x)).ToList();
                p1.ForEach(x => repository.Save(x));

                // retrieve again
                Persistable? element = repository.Get<Persistable>("p1_3");
                Assert.NotNull(element);

                // delete
                Assert.Null(element!.Payload);
                element!.Payload = "foobar";
                repository.Save(element);

                element = repository.Get<Persistable>("p1_3");
                Assert.NotNull(element);
                Assert.Equal("foobar", element!.Payload);
            }
            finally
            {
                repository.Truncate();
            }
        }

        [Theory]
        [InlineData(typeof(InMemoryRepository))]
        [InlineData(typeof(FileSystemRepository))]
        [InlineData(typeof(CachedRepository))]
        public void Truncate_Empty_Empty(Type repositoryType)
        {
            IServiceProvider services = createServiceProvider();
            IRepository repository = (IRepository)services.GetRequiredService(repositoryType);

            repository.Truncate();
            List<Persistable> lst = repository.GetAll<Persistable>();
            Assert.NotNull(lst);
            Assert.Empty(lst);
        }

        [Theory]
        [InlineData(typeof(InMemoryRepository))]
        [InlineData(typeof(FileSystemRepository))]
        [InlineData(typeof(CachedRepository))]
        public void Truncate_Not_Empty(Type repositoryType)
        {
            IServiceProvider services = createServiceProvider();
            IRepository repository = (IRepository)services.GetRequiredService(repositoryType);

            // create some elements and save them
            List<Persistable> p1 = Enumerable.Range(0, 10).Select(x => new Persistable("p1_" + x)).ToList();
            p1.ForEach(x => repository.Save(x));

            repository.Truncate();
            List<Persistable> lst = repository.GetAll<Persistable>();
            Assert.NotNull(lst);
            Assert.Empty(lst);
        }

        [Theory]
        [InlineData(typeof(InMemoryRepository))]
        [InlineData(typeof(FileSystemRepository))]
        [InlineData(typeof(CachedRepository))]
        public void Truncate_SaveNewAfterTruncate_Operable(Type repositoryType)
        {
            IServiceProvider services = createServiceProvider();
            IRepository repository = (IRepository)services.GetRequiredService(repositoryType);

            try
            {
                // create some elements and save them
                List<Persistable> p1 = Enumerable.Range(0, 10).Select(x => new Persistable("p1_" + x)).ToList();
                p1.ForEach(x => repository.Save(x));

                repository.Truncate();

                List<Persistable> lst = repository.GetAll<Persistable>();
                Assert.NotNull(lst);
                Assert.Empty(lst);

                p1.ForEach(x => repository.Save(x));

                lst = repository.GetAll<Persistable>();
                Assert.NotNull(lst);
                Assert.NotEmpty(lst);
            }
            finally
            {
                repository.Truncate();
            }
        }
    }
}
