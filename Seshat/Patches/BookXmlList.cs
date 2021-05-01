using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoMod;
using Registrar = Seshat.API.Registrar;

class patch_BookXmlList : BookXmlList
{
#pragma warning disable CS0169
    [MonoModRemove]
    private List<BookXmlInfo> _list;
    [MonoModRemove]
    private Dictionary<int, BookXmlInfo> _dictionary;
#pragma warning restore CS0169

    [MonoModReplace]
    public new void Init(List<BookXmlInfo> list)
    {
        foreach (BookXmlInfo book in list)
            Registrar.KeyPage.AddVanilla(book);
    }

    [MonoModReplace]
    public new BookXmlInfo GetData(int id)
    {
        return Registrar.KeyPage.Get(id);
    }

    [MonoModReplace]
    public new List<BookXmlInfo> GetList()
    {
        return Registrar.KeyPage.All().ToList();
    }

    [MonoModConstructor]
    [MonoModReplace]
    public void ctor() { }
}
