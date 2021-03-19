using System.IO;
using System.Xml.Serialization;
using LOR_DiceSystem;
using Seshat.API;
using Seshat.Module;

namespace Seshat
{
    public static class SeshatModuleExt
    {
        
        // HELPER FUNCTIONS

        /// <summary>
        /// Registers a combat page to Seshat.
        /// </summary>
        /// <param name="card">The combat page to register.</param>
        /// <exception cref="System.ArgumentException">
        /// The model shares a string id with an already registered card.
        /// </exception>
        public static void RegisterCombatPage(this SeshatModule module, DiceCardXmlInfo card)
        {
            // translate domain
            card.SetSID(StringId.Concat(module.Metadata.id, card.GetSID()));

            // register
            DiceCardRegistrar.Register(card);
        }

        /// <summary>
        /// Registers a combat page to Seshat from a bundle file.
        /// </summary>
        /// <param name="bundlePath">The path of the XML file.</param>
        /// <exception cref="System.ArgumentException">
        /// The model shares a string id with an already registered card.
        /// </exception>
        public static void RegisterCombatPage(this SeshatModule module, string bundlePath)
        {
            var xml = new XmlSerializer(typeof(DiceCardXmlInfo));

            using (Stream stream = module.Bundle.GetFile(bundlePath))
            {
                if (stream == null)
                    throw new System.Exception("Failed to find bundle file!");

                RegisterCombatPage(module, (DiceCardXmlInfo)xml.Deserialize(stream));
            }
        }
    }
}
