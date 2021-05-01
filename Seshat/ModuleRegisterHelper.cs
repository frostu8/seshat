using System;
using System.IO;
using System.Xml.Serialization;
using LOR_DiceSystem;
using LOR_XML;
using Registrar = Seshat.API.Registrar;
using Seshat.API;
using Seshat.Attribute;
using Seshat.Module;

namespace Seshat
{
    /// <summary>
    /// Provides functions for mods to register their content.
    /// </summary>
    public class ModuleRegisterHelper
    {
        private SeshatModule module;

        public ModuleRegisterHelper(SeshatModule module)
        {
            this.module = module;
        }

        public void CombatPages(DiceCardXmlRoot root)
        {
            foreach (var card in root.cardXmlList)
                SingleCombatPage(card);
        }

        public void SingleCombatPage(DiceCardXmlInfo card)
        {
            card.SetId(StringId.HasDomainOr(card.GetId(), module.Metadata.Domain));
            Registrar.CombatPage.AddModded(card);
        }

        public void CombatPages(string bundlePath)
            => CombatPages(XmlFromBundle<DiceCardXmlRoot>(bundlePath));

        public void KeyPages(BookXmlRoot root)
        {
            foreach (var book in root.bookXmlList)
                SingleKeyPage(book);
        }

        public void SingleKeyPage(BookXmlInfo book)
        {
            book.SetId(StringId.HasDomainOr(book.GetId(), module.Metadata.Domain));
            Registrar.KeyPage.AddModded(book);
        }

        public void KeyPages(string bundlePath)
            => KeyPages(XmlFromBundle<BookXmlRoot>(bundlePath));

        public DiceCardXmlInfo GetCombatPage(string sid)
            => Registrar.CombatPage.Get(StringId.Concat(module.Metadata.Domain, sid));

        public BookXmlInfo GetKeyPage(string sid)
            => Registrar.KeyPage.Get(StringId.Concat(module.Metadata.Domain, sid));

        public void LocalizeCombatPages(BattleCardDescRoot root)
        {
            foreach (var card in root.cardDescList)
                LocalizeSingleCombatPage(card);
        }

        public void LocalizeSingleCombatPage(BattleCardDesc card)
        {
            card.SetId(StringId.HasDomainOr(card.GetId(), module.Metadata.Domain));
            Registrar.Localize.CombatPage.AddModded(card);
        }

        public void LocalizeCombatPages(string bundlePath)
            => LocalizeCombatPages(XmlFromBundle<BattleCardDescRoot>(bundlePath));

        public void LocalizeDiceAbilities(BattleCardAbilityDescRoot root)
        {
            foreach (var desc in root.cardDescList)
                LocalizeSingleDiceAbility(desc);
        }

        public void LocalizeSingleDiceAbility(BattleCardAbilityDesc desc)
        {
            desc.SetId(StringId.HasDomainOr(desc.GetId(), module.Metadata.Domain));
            Registrar.Localize.DiceAbility.AddModded(desc);
        }

        public void LocalizeDiceAbilities(string bundlePath)
            => LocalizeDiceAbilities(XmlFromBundle<BattleCardAbilityDescRoot>(bundlePath));

        public void LocalizeCardAbilities(BattleCardAbilityDescRoot root)
        {
            foreach (var desc in root.cardDescList)
                LocalizeSingleCardAbility(desc);
        }

        public void LocalizeSingleCardAbility(BattleCardAbilityDesc desc)
        {
            desc.SetId(StringId.HasDomainOr(desc.GetId(), module.Metadata.Domain));
            Registrar.Localize.CardAbility.AddModded(desc);
        }

        public void LocalizeCardAbilities(string bundlePath)
            => LocalizeCardAbilities(XmlFromBundle<BattleCardAbilityDescRoot>(bundlePath));

        public void LocalizePassiveAbilities(PassiveDescRoot root)
        {
            foreach (var passive in root.descList)
                LocalizeSinglePassiveAbility(passive);
        }

        public void LocalizeSinglePassiveAbility(PassiveDesc passive)
        {
            passive.SetId(StringId.HasDomainOr(passive.GetId(), module.Metadata.Domain));
            Registrar.Localize.Passive.AddModded(passive);
        }

        public void LocalizePassiveAbilities(string bundlePath)
            => LocalizePassiveAbilities(XmlFromBundle<PassiveDescRoot>(bundlePath));

        private T XmlFromBundle<T>(string path)
        {
            var xml = new XmlSerializer(typeof(T));

            using (Stream stream = module.Bundle.GetFile(path))
            {
                if (stream == null)
                    throw new FileNotFoundException("Failed to find bundle file!", path);
                else
                    return (T)xml.Deserialize(stream);
            }
        }
    }
}
