using MedEl.Vehicles.Model.Interfaces;
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
        public FileSystemRepository(string rootDirectory, ISerializer serializer)
        {
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
        public bool Delete<TEntity>(TEntity entity) where TEntity : IPersistable
        {
            return Delete<TEntity>(entity.Id);
        }

        /// <inheritdoc/>
        public bool Delete<TEntity>(string id) where TEntity : IPersistable
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
        public TEntity? Get<TEntity>(string id) where TEntity : IPersistable
        {
            string fileName = getPath<TEntity>(id);
            if (!File.Exists(fileName))
            {
                return default(TEntity?);
            }
            return read<TEntity>(fileName);
        }

        /// <inheritdoc/>
        public List<TEntity> GetAll<TEntity>() where TEntity : IPersistable
        {
            return Directory.EnumerateFiles(getPath<TEntity>())
                .Select(x => read<TEntity>(x))
                .ToList();
        }

        /// <inheritdoc/>
        public void Save<TEntity>(TEntity entity) where TEntity : IPersistable
            => write(entity);

        private void write<TEntity>(TEntity entity) where TEntity : IPersistable
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
            if (string.IsNullOrWhiteSpace(id))
            {
                return Path.Combine(rootDirectory, typeof(TEntity).Name);
            }

            string typePath = getPath<TEntity>();
            return Path.Combine(typePath, $"{id}{serializer.FileExtension}");
        }
    }
}
