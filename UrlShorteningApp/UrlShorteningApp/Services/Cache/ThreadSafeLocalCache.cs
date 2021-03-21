using System.Collections.Generic;
using System.Linq;

namespace UrlShorteningApp.Services.Cache
{
    /// <summary>
    /// Thread Safe Local Cache
    /// </summary>
    public class ThreadSafeLocalCache : ILocalCache
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The object
        /// </returns>
        public T Get<T>(string key)
        {
            return (T) ThreadSafeStore.Instance.Get(key);
        }

        /// <summary>
        /// Get All values from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>()
        {
            return ThreadSafeStore.Instance.GetAll().Select(item => (T)item);
        }

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Put<T>(string key, T value)
        {
            ThreadSafeStore.Instance.Put(key, value);
        }
    }

    /// <summary>
    /// Thread Safe Store (singleton)
    /// </summary>
    internal class ThreadSafeStore
    {
        #region Singleton

        /// <summary>
        /// Locking object for thread safety
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// Instance of the <see cref="ThreadSafeStore"/>
        /// </summary>
        private static volatile ThreadSafeStore instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="ThreadSafeStore"/> class from being created.
        /// </summary>
        private ThreadSafeStore()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static ThreadSafeStore Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ThreadSafeStore();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion

        /// <summary>
        /// Lock object for the store.
        /// </summary>
        private readonly object storeLocker = new object();

        /// <summary>
        /// The store of diagnostic data.
        /// </summary>
        private readonly Dictionary<string, object> store = new Dictionary<string, object>();

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The object
        /// </returns>
        public object Get(string key)
        {
            lock (this.storeLocker)
            {
                return this.store.ContainsKey(key) ? this.store[key] : null;
            }
        }

        public IEnumerable<object> GetAll()
        {
            lock (this.storeLocker)
            {
                return this.store.Select(item => item.Value);
            }
        }

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Put(string key, object value)
        {
            lock (this.storeLocker)
            {
                if (this.store.ContainsKey(key))
                {
                    this.store[key] = value;
                }
                else
                {
                    this.store.Add(key, value);
                }
            }
        }
    }
}