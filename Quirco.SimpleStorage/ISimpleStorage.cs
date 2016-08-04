namespace Quirco.SimpleStorage
{
    public interface ISimpleStorage
    {
        /// <summary>
        ///     Delete the specified key.
        /// </summary>
        void Delete(string key);

        /// <summary>
        ///     Determines whether the storage has the specified key.
        /// </summary>
        /// <returns><c>true</c> if this instance has the specified key; otherwise, <c>false</c>.</returns>
        /// <param name="key">Key.</param>
        bool HasKey(string key);

        /// <summary>
        ///     Persists a complex value with given key via binary serialization.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">must be serializable</param>
        void Put<T>(string key, T value);

        /// <summary>
        ///     Get the specified key or the dafault value of type T if the key does not exist.
        /// </summary>
        /// <remarks>Use HasKey(key) beforehand if T is a Value Type and you do not want the default value.</remarks>
        /// <param name="key">Key.</param>
        /// <returns>deserialized complex type</returns>
        T Get<T>(string key);

        /// <summary>
        ///     Retrives a value with given key. If key can not be found, defaultValue is returned.
        /// </summary>
        /// <returns>deserialized complex type</returns>
        T Get<T>(string key, T defaultValue);
    }
}