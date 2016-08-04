using Android.Content;

namespace Quirco.SimpleStorage.Android
{
    public class AndroidSimpleStorage : SimpleStorage
    {
        readonly ISharedPreferences _prefs;

        public AndroidSimpleStorage(Context context, string @group = null) : base(@group)
        {
            _prefs = context.GetSharedPreferences(group, FileCreationMode.Private);
        }

        /// <summary>
        /// Retrieves value with given key.
        /// </summary>
        /// <returns>null, if key can not be found</returns>
        public override string Get(string key)
        {
            return _prefs.GetString(key, null);
        }

        /// <summary>
        /// Persists a value with given key.
        /// </summary>
        /// <param name = "key">Key</param>
        /// <param name="value">if value is null, the key will be deleted</param>
        public override void Put(string key, string value)
        {
            _prefs.Edit().PutString(key, value).Commit();
        }

        /// <summary>
        /// Delete the specified key.
        /// </summary>
        public override void Delete(string key)
        {
            _prefs.Edit().PutString(key, null).Commit();
        }
    }
}