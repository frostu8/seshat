using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seshat.API;

namespace Seshat
{
    /// <summary>
    /// A set of definitions to easily implement registrar functionality. Use
    /// this for models that are identified by strings in the vanilla game.
    /// </summary>
    internal class SidModelRegistrar<T> where T: class {
        private Dictionary<string, T> _models = new Dictionary<string, T>();

        public Dictionary<string, T> ModelDict => _models;

        public T Get(string sid)
            => _models.TryGetValue(sid, out T value) ? value : null;

        public void Add(string sid, T model)
            => _models.Add(sid, model);

        public IEnumerable<T> ByDomain(string domain)
        {
            return _models
                .Where(kvp => StringId.InDomain(kvp.Key, domain))
                .Select(kvp => kvp.Value);
        }

        public IEnumerable<T> All()
            => _models.Values;
    }
}
