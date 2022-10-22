using MedEl.Vehicles.Common.Configuration;
using MedEl.Vehicles.Common.Repository;
using MedEl.Vehicles.Repository.FileSystem;
using MedEl.Vehicles.Repository.InMemory;
using MedEl.Vehicles.Repository.PseudoRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.Tests.Factory
{
    public class RepositoryFactoryTests : TestBase
    {
        [Fact]
        public void CreateInstance_Defaults_InMemory()
        {
            IServiceProvider services = createServiceProvider();
            IRepository repository = services.GetRequiredService<IRepository>();

            Assert.NotNull(repository);
            Assert.IsType<InMemoryRepository>(repository);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CreateInstance_NoFileSystemPath_InMemory(bool useCaching)
        {
            IServiceProvider services = createServiceProvider();

            IConfigurationDictionary config = services.GetRequiredService<IConfigurationDictionary>();
            config["Repository:UseCaching"] = useCaching.ToString();

            IRepository repository = services.GetRequiredService<IRepository>();

            Assert.NotNull(repository);
            Assert.IsType<InMemoryRepository>(repository);
        }

        [Fact]
        public void CreateInstance_FileSystemPathNoCaching_FileSystemRepository()
        {
            IServiceProvider services = createServiceProvider();

            IConfigurationDictionary config = services.GetRequiredService<IConfigurationDictionary>();

            string repositoryDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            config["Repository:FileSystemRepositoryPath"] = repositoryDir;
            config["Repository:UseCaching"] = "false";

            try
            {
                IRepository repository = services.GetRequiredService<IRepository>();

                Assert.NotNull(repository);
                Assert.IsType<FileSystemRepository>(repository);

                Assert.True(Directory.Exists(repositoryDir));
            }
            finally
            {
                Directory.Delete(repositoryDir);
            }
        }

        [Fact]
        public void CreateInstance_FileSystemPathNoCaching_CachedRepository()
        {
            IServiceProvider services = createServiceProvider();

            IConfigurationDictionary config = services.GetRequiredService<IConfigurationDictionary>();

            string repositoryDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            config["Repository:FileSystemRepositoryPath"] = repositoryDir;
            config["Repository:UseCaching"] = "true";

            IRepository repository = services.GetRequiredService<IRepository>();

            Assert.NotNull(repository);
            Assert.IsType<CachedRepository>(repository);

            Assert.True(Directory.Exists(repositoryDir));
        }
    }
}
