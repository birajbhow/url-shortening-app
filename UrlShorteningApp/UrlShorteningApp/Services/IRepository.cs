using System.Collections.Generic;
using UrlShorteningApp.Models;

namespace UrlShorteningApp.Services
{
    public interface IRepository
    {
        IEnumerable<UrlModel> GetUrls();

        void AddUrl(UrlModel urlModel);

        string FindUrl(string code);
    }
}
