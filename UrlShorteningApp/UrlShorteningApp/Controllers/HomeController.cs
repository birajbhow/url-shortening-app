using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UrlShorteningApp.Models;
using UrlShorteningApp.Services;

namespace UrlShorteningApp.Controllers
{   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;
        private readonly IUrlService _urlService;
        private readonly string _baseUrl;

        public HomeController(ILogger<HomeController> logger,
            IRepository repository, IUrlService urlService, IConfiguration configuration)
        {
            _logger = logger;
            _repository = repository;
            _urlService = urlService;
            _baseUrl = configuration["BaseUrl"];
        }

        public IActionResult Index(string code)
        {   
            var model = _repository.GetUrls();
            return View(model);
        }

        [HttpPost]
        public IActionResult ShortenUrl(string originalUrl)
        {
            if (!_urlService.Parse(originalUrl))
            {
                return RedirectToAction(nameof(Error));
            }

            var shortenedCode = _urlService.GenerateCode(originalUrl);
            var model = new UrlModel 
            { 
                OriginalUrl = originalUrl, 
                ShortenedUrl = $"{_baseUrl}{shortenedCode}", 
                Code = shortenedCode 
            };
            _repository.AddUrl(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RedirectToOriginal(string code)
        {   
            var originalUrl = _repository.FindUrl(code);
            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                return RedirectToAction(nameof(Error));
            }

            return Redirect(originalUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
