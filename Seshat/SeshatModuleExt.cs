using System.IO;
using System.Xml.Serialization;
using LOR_DiceSystem;
using Seshat.API;
using Seshat.Module;

namespace Seshat
{
    public static class SeshatModuleExt
    {
        /// <summary>
        /// Registers all combat pages in a bundle to Seshat.
        /// </summary>
        /// <param name="root">The combat pages to register.</param>
        /// <exception cref="System.ArgumentException">
        /// One of the models share a string id with an already registered card.
        /// </exception>
        public static void RegisterCombatPages(this SeshatModule module, DiceCardXmlRoot root)
        {
            // it is important to do these seperately just in case there is an
            // error in the xml definitions
            // translate domain
            foreach (var card in root.cardXmlList)
                card.SetSID(StringId.Concat(module.Metadata.id, card.GetSID()));

            // register
            foreach (var card in root.cardXmlList)
                DiceCardRegistrar.Register(card);
        }

        /// <summary>
        /// Registers all combat pages in a bundle to Seshat from a file.
        /// </summary>
        /// <param name="bundlePath">The path of the XML file.</param>
        /// <exception cref="System.ArgumentException">
        /// One of the models share a string id with an already registered card.
        /// </exception>
        public static void RegisterCombatPages(this SeshatModule module, string bundlePath)
        {
            var xml = new XmlSerializer(typeof(DiceCardXmlRoot));

            using (Stream stream = module.Bundle.GetFile(bundlePath))
            {
                if (stream == null)
                    throw new System.Exception("Failed to find bundle file!");

                RegisterCombatPages(module, (DiceCardXmlRoot)xml.Deserialize(stream));
            }
        }

        public static DiceCardXmlInfo GetCombatPage(this SeshatModule module, string sid)
        {
            return DiceCardRegistrar.Get(StringId.Concat(module.Metadata.id, sid));
        }
    }
}
