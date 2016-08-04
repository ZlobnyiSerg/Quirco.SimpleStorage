using Foundation;

namespace Quirco.SimpleStorage.iOS
{
    public class iOsSimpleStorage : SimpleStorage
    {
        /// <summary>
        ///     Persists a value with given key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">if value is null, the key will be deleted</param>
        public override void Put(string key, string value)
        {
            if (value == null)
                Delete(key);
            else
                NSUserDefaults.StandardUserDefaults.SetValueForKey(new NSString(value), new NSString(Group + "_" + key));
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        /// <summary>
        ///     Retrieves value with given key.
        /// </summary>
        /// <returns>null, if key can not be found</returns>
        public override string Get(string key)
        {
            return NSUserDefaults.StandardUserDefaults.StringForKey(Group + "_" + key);
        }

        /// <summary>
        ///     Delete the specified key.
        /// </summary>
        public override void Delete(string key)
        {
            NSUserDefaults.StandardUserDefaults.RemoveObject(new NSString(Group + "_" + key));
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }
    }
}