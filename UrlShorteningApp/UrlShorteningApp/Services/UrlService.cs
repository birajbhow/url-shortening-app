using Microsoft.Extensions.Logging;
using System;

namespace UrlShorteningApp.Services
{
    public class UrlService : IUrlService
    {
        private readonly ILogger<UrlService> _logger;

        public UrlService(ILogger<UrlService> logger)
        {
            _logger = logger;
        }
        public string GenerateCode(string url)
        {
            return url.GetHashCode().ToString("x");
        }

        public bool Parse(string url)
        {
            try
            {
                return new Uri(url).IsWellFormedOriginalString();
            } 
            catch(Exception ex)
            {
                _logger.LogError(nameof(UrlService), ex);
                return false;
            }
        }
    }
}
