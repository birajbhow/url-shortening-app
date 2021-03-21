using System.Collections.Generic;

namespace UrlShorteningApp.Services.Cache
{
    /// <summary>
    /// Local cache interface
    /// </summary>
    public interface ILocalCache
    {
        T Get<T>(string key);

        void Put<T>(string key, T value);

        IEnumerable<T> GetAll<T>();
    }
}
