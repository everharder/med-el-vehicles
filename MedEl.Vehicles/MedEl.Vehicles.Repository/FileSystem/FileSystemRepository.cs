using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Common.Repository;
using MedEl.Vehicles.Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.FileSystem
{
    internal class FileSystemRepository : IRepository
    {
        private readonly string rootDirectory;
        private readonly ISerializer serializer;

        /// <summary>
        /// Create a new instance of the filerepository
        /// </summary>
        /// <param name="rootDirectory">The directory the repository exists in</param>
        /// <param name="serializer">The serializer to use</param>
        public FileSystemRepository(RepositoryConfiguration configuration, ISerializer serializer)
        {
            string? rootDirectory = configuration.FileSystemRepositoryPath;
            if (string.IsNullOrWhiteSpace(rootDirectory))
            {
                throw new ArgumentException($"'{nameof(rootDirectory)}' cannot be null or whitespace.", nameof(rootDirectory));
            }

            // if this is a relative path, prepend the current directory
            if (!Path.IsPathRooted(rootDirectory))
            {
                rootDirectory = Path.Combine(Environment.CurrentDirectory, rootDirectory);
            }

            if (!Directory.Exists(rootDirectory))
            {
                Directory.CreateDirectory(rootDirectory);
            }

            this.rootDirectory = rootDirectory;
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        /// <inheritdoc/>
        public bool Delete<TEntity>(TEntity entity) where TEntity : IIdentification
        {
            return Delete<TEntity>(entity.Id);
        }

        /// <inheritdoc/>
        public bool Delete<TEntity>(string id) where TEntity : IIdentification
        {
            string fileName = getPath<TEntity>(id);
            lock (fileName)
            {
                if (!File.Exists(fileName))
                {
                    return false;
                }
                File.Delete(fileName);
            }
            return true;
        }

        /// <inheritdoc/>
        public TEntity? Get<TEntity>(string id) where TEntity : IIdentification
        {
            string fileName = getPath<TEntity>(id);
            if (!File.Exists(fileName))
            {
                return default(TEntity?);
            }
            return read<TEntity>(fileName);
        }

        /// <inheritdoc/>
        public List<TEntity> GetAll<TEntity>() where TEntity : IIdentification
        {
            return Directory.EnumerateFiles(getPath<TEntity>())
                .Select(x => read<TEntity>(x))
                .ToList();
        }

        /// <inheritdoc/>
        public void Save<TEntity>(TEntity entity) where TEntity : IIdentification
            => write(entity);

        /// <inheritdoc/>
        public void Truncate()
        {
            Directory.Delete(rootDirectory, recursive: true);
        }

        private void write<TEntity>(TEntity entity) where TEntity : IIdentification
        {
            string content = serializer.Serialize(entity);
            string fileName = getPath<TEntity>(entity.Id);
            lock (fileName)
            {
                File.WriteAllText(fileName, content);
            }
        }

        private TEntity read<TEntity>(string fileName)
        {
            lock (fileName)
            {
                string content = File.ReadAllText(fileName);
                return serializer.Deserialize<TEntity>(content);
            }
        }

        private string getPath<TEntity>(string? id = null)
        {
            // check if root exists (might have been deleted beforehand)
            if (!Directory.Exists(rootDirectory))
            {
                Directory.CreateDirectory(rootDirectory);
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                string directoryForType = Path.Combine(rootDirectory, typeof(TEntity).Name);
                if(!Directory.Exists(directoryForType))
                {
                    Directory.CreateDirectory(directoryForType);
                }
                return directoryForType;
            }

            string typePath = getPath<TEntity>();
            return Path.Combine(typePath, $"{id}{serializer.FileExtension}");
        }

        /// <inheritdoc/>
        public string GetHighestId<TEntity>() where TEntity : IIdentification
        {
            string directory = getPath<TEntity>();
            return Directory.EnumerateFiles(directory)
                .Select(x => Path.GetFileNameWithoutExtension(x))
                .Select(x => long.TryParse(x, out long id) ? id : (long?)null)
                .Where(x => x.HasValue)
                .OrderByDescending(x => x)
                .FirstOrDefault()?.ToString() ?? "-1";
        }
    }
}
