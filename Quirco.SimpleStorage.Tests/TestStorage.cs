using System.Collections.Generic;

namespace Quirco.SimpleStorage.Tests
{
    public class TestStorage : SimpleStorage
    {
        private readonly Dictionary<string, string> _cache = new Dictionary<string, string>(); 
        public override void Delete(string key)
        {
            _cache.Remove(key);
        }

        public override void Put(string key, string value)
        {
            _cache[key] = value;
        }

        public override string Get(string key)
        {
            return _cache[key];
        }
    }
}