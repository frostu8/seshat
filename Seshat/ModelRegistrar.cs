using System;
using System.Collections.Generic;
using System.Linq;
using Seshat.API;

namespace Seshat {
    /// <summary>
    /// A set of definitions to easily implement registrar functionality.
    /// </summary>
    internal class ModelRegistrar<T> where T: class {
        private Dictionary<int, T> _models = new Dictionary<int, T>();

        private Dictionary<string, T> _modelsBySid = new Dictionary<string, T>();

        public Dictionary<int, T> ModelDict => _models;

        public T Get(string sid)
            => _modelsBySid.TryGetValue(sid, out T value) ? value : null;

        public T Get(int id)
            => _models.TryGetValue(id, out T value) ? value : null;

        public void Add(int id, T model, string sid = null)
        {
            _models.Add(id, model);

            if (sid != null)
                _modelsBySid.Add(sid, model);
        }

        public IEnumerable<T> ByDomain(string domain)
        {
            return _modelsBySid
                .Where(kvp => StringId.InDomain(kvp.Key, domain))
                .Select(kvp => kvp.Value);
        }

        public IEnumerable<T> All()
            => _models.Values;
    }
}
