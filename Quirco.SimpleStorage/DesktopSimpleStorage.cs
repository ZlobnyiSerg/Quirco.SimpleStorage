using System.Collections.Generic;

namespace Quirco.SimpleStorage
{
    public class DesktopSimpleStorage : SimpleStorage
    {
        private static readonly Dictionary<string, string> Database = new Dictionary<string, string>();

        private static readonly object LockObject = new object();

        public DesktopSimpleStorage(string groupName) : base(groupName)
        {
        }

        /// <summary>
        ///     Persists a value with given key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">if value is null, the key will be deleted</param>
        public override void Put(string key, string value)
        {
            if (value == null)
            {
                Delete(key);
                return;
            }

            lock (LockObject)
            {
                var id = Group + "_" + key;
                if (Database.ContainsKey(id))
                    Database.Remove(id);

                Database.Add(id, value);
            }
        }

        /// <summary>
        ///     Retrieves value with given key.
        /// </summary>
        /// <returns>null, if key can not be found</returns>
        public override string Get(string key)
        {
            lock (LockObject)
            {
                var id = Group + "_" + key;
                return Database.ContainsKey(id) ? Database[id] : null;
            }
        }

        /// <summary>
        ///     Delete the specified key.
        /// </summary>
        public override void Delete(string key)
        {
            lock (LockObject)
            {
                Database.Remove(Group + "_" + key);
            }
        }        
    }
}