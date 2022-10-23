using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.FileSystem
{
    internal interface ISerializer
    {
        /// <summary>
        /// The extension to add to a file of the serializer outputtype, including the leading dot (e.g. '.json', '.xml')
        /// </summary>
        public string FileExtension { get; }

        /// <summary>
        /// Converts the given string content to an instance of <paramref name="content"/>
        /// </summary>
        public TEntity Deserialize<TEntity>(string content);

        /// <summary>
        /// Serializes <paramref name="element"/> to string
        /// </summary>
        public string Serialize<TEntity>(TEntity element);
    }
}
