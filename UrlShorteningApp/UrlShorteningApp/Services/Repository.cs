using System.Collections.Generic;
using UrlShorteningApp.Models;
using UrlShorteningApp.Services.Cache;

namespace UrlShorteningApp.Services
{
    public class Repository : IRepository
    {
        private readonly ILocalCache _localCache;

        public Repository(ILocalCache localCache)
        {
            _localCache = localCache;
        }

        public void AddUrl(UrlModel urlModel)
        {
            _localCache.Put(urlModel.Code, urlModel);
        }

        public string FindUrl(string code)
        {
            return _localCache.Get<UrlModel>(code)?.OriginalUrl;
        }

        public IEnumerable<UrlModel> GetUrls()
        {
            return _localCache.GetAll<UrlModel>();
        }
    }
}
