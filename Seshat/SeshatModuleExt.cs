using System;
using System.IO;
using System.Xml.Serialization;
using LOR_DiceSystem;
using LOR_XML;
using Seshat.API;
using Seshat.Attribute;
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
            foreach (var card in root.cardXmlList)
                RegisterSingleCombatPage(module, card);
        }

        /// <summary>
        /// Registers a combat page to Seshat.
        /// </summary>
        /// <param name="card">The combat page to register.</param>
        /// <exception cref="System.ArgumentException">
        /// One of the combat pages share a string id with an already registered 
        /// card.
        /// </exception>
        public static void RegisterSingleCombatPage(this SeshatModule module, DiceCardXmlInfo card)
        {
            card.SetSID(StringId.HasDomainOr(card.GetSID(), module.Metadata.Domain));
            DiceCardRegistrar.AddModded(card);
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
            return DiceCardRegistrar.Get(StringId.Concat(module.Metadata.Domain, sid));
        }

        public static void RegisterCombatPagesLocalization(
            this SeshatModule module, BattleCardDescRoot root)
        {
            foreach (var card in root.cardDescList)
                RegisterSingleCombatPageLocalization(module, card);
        }

        public static void RegisterSingleCombatPageLocalization(
            this SeshatModule module, BattleCardDesc card)
        {
            card.SetSID(StringId.HasDomainOr(card.GetSID(), module.Metadata.Domain));
            DiceCardLocalizeRegistrar.AddModded(card);
        }

        public static void RegisterCombatPagesLocalization(
            this SeshatModule module, string bundlePath)
        {
            var xml = new XmlSerializer(typeof(BattleCardDescRoot));

            using (Stream stream = module.Bundle.GetFile(bundlePath))
            {
                if (stream == null)
                    throw new System.Exception("Failed to find bundle file!");

                RegisterCombatPagesLocalization(module, (BattleCardDescRoot)xml.Deserialize(stream));
            }
        }

        public static void RegisterDiceAbilitiesLocalization(
            this SeshatModule module, BattleCardAbilityDescRoot root)
        {
            foreach (var desc in root.cardDescList)
                RegisterSingleDiceAbilityLocalization(module, desc);
        }

        public static void RegisterSingleDiceAbilityLocalization(
            this SeshatModule module, BattleCardAbilityDesc desc)
        {
            desc.SetSID(StringId.HasDomainOr(desc.GetSID(), module.Metadata.Domain));
            DiceCardAbilityLocalizeRegistrar.AddModded(desc);
        }

        public static void RegisterDiceAbilitiesLocalization(
            this SeshatModule module, string bundlePath)
        {
            var xml = new XmlSerializer(typeof(BattleCardAbilityDescRoot));

            using (Stream stream = module.Bundle.GetFile(bundlePath))
            {
                if (stream == null)
                    throw new System.Exception("Failed to find bundle file!");

                RegisterDiceAbilitiesLocalization(module, (BattleCardAbilityDescRoot)xml.Deserialize(stream));
            }
        }
    }
}
