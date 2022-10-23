using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.FileSystem
{
    /// <summary>
    /// JSON serializer for <see cref="FileSystemRepository"/>
    /// </summary>
    internal class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings settings;

        public string FileExtension => ".json";

        public JsonSerializer()
        {
            this.settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        /// <inheritdoc/>
        public TEntity Deserialize<TEntity>(string content)
        {
            TEntity? result = (TEntity?)JsonConvert.DeserializeObject(content, settings);  
            if(result == null)
            {
                throw new IOException($"Could not read JSON content for type {typeof(TEntity).FullName} from '{content}'");
            }
            return result;
        }

        /// <inheritdoc/>
        public string Serialize<TEntity>(TEntity element)
        {
            return JsonConvert.SerializeObject(element, settings);
        }
    }
}
