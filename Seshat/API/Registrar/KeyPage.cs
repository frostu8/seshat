using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seshat.API.Registrar
{
    public static class KeyPage
    {
        private static Dictionary<int, BookXmlInfo> _pages = 
            new Dictionary<int, BookXmlInfo>();
        private static ModelDictionary<BookXmlInfo> _pagesBySid = 
            new ModelDictionary<BookXmlInfo>();

        internal static void AddVanilla(BookXmlInfo book)
        {
            // hash up a string id for the card
            book.SetId(Seshat.HashNumericId(book.id));

            Add(book);
        }

        internal static void AddModded(BookXmlInfo book)
        {
            // generate a unique id for the card
            int generatedId = IdGen.NextFree(id => !_pages.ContainsKey(id));
            book.id = generatedId;

            Add(book);
        }

        private static void Add(BookXmlInfo book)
        {
            NormalizePageReferences(book);

            _pages.Add(book.id, book);
            _pagesBySid.Add(book.GetId(), book);
        }

        private static void NormalizePageReferences(BookXmlInfo book)
        {
            string domain = StringId.GetDomain(book.GetId());

            // normalize ModPassive
            ((patch_BookEquipEffect)book.EquipEffect).passiveModList =
                book.EquipEffect.GetPassiveModList()
                    .Select(s => StringId.HasDomainOr(s, domain))
                    .ToList();
        }
    }
}
