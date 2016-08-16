using System;
using Newtonsoft.Json;

namespace Quirco.SimpleStorage
{
    public abstract class SimpleStorage : ISimpleStorage
    {
        protected readonly string Group;

        protected SimpleStorage(string group = null)
        {
            Group = group;
        }

        /// <summary>
        ///     Delete the specified key.
        /// </summary>
        public abstract void Delete(string key);

        /// <summary>
        ///     Determines whether the storage has the specified key.
        /// </summary>
        /// <returns><c>true</c> if this instance has the specified key; otherwise, <c>false</c>.</returns>
        /// <param name="key">Key.</param>
        public bool HasKey(string key)
        {
            return Get(key) != null;
        }

        /// <summary>
        ///     Persists a complex value with given key via binary serialization.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">must be serializable</param>
        public void Put<T>(string key, T value)
        {
            Put(key, SerializeObject(value));
        }

        /// <summary>
        ///     Get the specified key or the dafault value of type T if the key does not exist.
        /// </summary>
        /// <remarks>Use HasKey(key) beforehand if T is a Value Type and you do not want the default value.</remarks>
        /// <param name="key">Key.</param>
        /// <returns>deserialized complex type</returns>
        public T Get<T>(string key)
        {
            var data = Get(key);
            if (data == null)
                return default(T);

            return DeserializeObject<T>(data);           
        }

        /// <summary>
        ///     Retrives a value with given key. If key can not be found, defaultValue is returned.
        /// </summary>
        /// <returns>deserialized complex type</returns>
        public T Get<T>(string key, T defaultValue)
        {
            return HasKey(key) ? Get<T>(key) : defaultValue;
        }

        /// <summary>
        ///     Persists a value with given key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">if value is null, the key will be deleted</param>
        public abstract void Put(string key, string value);

        /// <summary>
        ///     Retrieves value with given key.
        /// </summary>
        /// <returns>null, if key can not be found</returns>
        public abstract string Get(string key);

        #region Helper methods

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            NullValueHandling = NullValueHandling.Ignore,
            FloatParseHandling = FloatParseHandling.Decimal,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        };

        protected virtual string SerializeObject<T>(T o)
        {
            return JsonConvert.SerializeObject(o, SerializerSettings);
        }

        protected virtual T DeserializeObject<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str, SerializerSettings);
        }

        #endregion
    }
}