using System;
using System.IO;
using System.Linq;
using Tommy;

namespace Seshat.Module
{
    /// <summary>
    /// Individual mod metadata.
    /// </summary>
    public class SeshatModuleMetadata
    {
        /// <summary>
        /// The mod's ID. This ID must be unique and must identify the module.
        /// </summary>
        public string id;

        /// <summary>
        /// A human-friendly identifier for the mod.
        /// </summary>
        public string name;

        /// <summary>
        /// The location of a DLL to optionally load.
        /// </summary>
        public string dll;

        public SeshatModuleMetadata(string id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return $"{this.id}";
        }

        #region SERIALIZATION

        internal class MetadataFormatException : Exception {
            public MetadataFormatException(string message) : base(message) { }
        }

        internal static SeshatModuleMetadata[] DeserializeAll(TextReader t)
        {
            return TOML.Parse(t).RawTable
                .Select(kv => Deserialize(kv.Key, kv.Value))
                .ToArray();
        }

        internal static SeshatModuleMetadata Deserialize(string id, TomlNode node)
        {
            // check if the node is a table.
            if (node is TomlTable table)
            {
                return new SeshatModuleMetadata(id)
                {
                    name = Required<TomlString>(table, "name").Value,
                    dll = Nullable<TomlString>(table, "dll")?.Value,
                };
            } 
            else
            {
                throw new MetadataFormatException($"\"{id}\" is not valid metadata.");
            }
        }

        private static T Required<T>(TomlTable table, string field) where T: TomlNode
        {
            T result = Nullable<T>(table, field);

            if (result != null) return result;
            else throw new MetadataFormatException(
                $"Field \"{field}\" of type {typeof(T).Name} required.");
        }

        private static T Nullable<T>(TomlTable table, string field) where T: TomlNode
        {
            if (table.TryGetNode(field, out TomlNode node))
            {
                if (node is T newNode) return newNode;
                else throw new MetadataFormatException(
                    $"Field \"{field}\" must be of type {typeof(T).Name}.");
            }
            else return null;
        }

        #endregion
    }
}
