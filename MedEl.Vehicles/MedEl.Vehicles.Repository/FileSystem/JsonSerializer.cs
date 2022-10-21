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
        public string FileExtension => ".json";

        /// <inheritdoc/>
        public TEntity Deserialize<TEntity>(string content)
        {
            TEntity? result = JsonConvert.DeserializeObject<TEntity>(content);  
            if(result == null)
            {
                throw new IOException($"Could not read JSON content for type {typeof(TEntity).FullName} from '{content}'");
            }
            return result;
        }

        /// <inheritdoc/>
        public string Serialize<TEntity>(TEntity element)
        {
            return JsonConvert.SerializeObject(element);
        }
    }
}
