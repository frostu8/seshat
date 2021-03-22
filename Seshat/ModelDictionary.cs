using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seshat.API;

namespace Seshat
{
    public class ModelDictionary<T> : Dictionary<string, T> where T: class
    {
        public T Get(string key)
            => this.TryGetValue(key, out T value) ? value : null;

        public IEnumerable<T> ByDomain(string domain)
        {
            return this
                .Where(kvp => StringId.InDomain(kvp.Key, domain))
                .Select(kvp => kvp.Value);
        }
    }
}
