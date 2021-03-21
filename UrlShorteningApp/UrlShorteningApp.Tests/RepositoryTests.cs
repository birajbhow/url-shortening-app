using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShorteningApp.Models;
using UrlShorteningApp.Services;
using UrlShorteningApp.Services.Cache;

namespace UrlShorteningApp.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        private IRepository subject;

        [SetUp]
        public void Setup()
        {
            subject = new Repository(new ThreadSafeLocalCache());
        }

        [Test]
        public void AddUrl_Works_Fine()
        {
            // arrange
            var model = CreateNewUrlModel("https://www.bbc.co.uk", "https://localhost/abc", "abc");

            // act
            subject.AddUrl(model);

            // assert
            Assert.AreEqual(model.OriginalUrl, subject.FindUrl(model.Code));
        }

        [Test]
        public void GetUrls_Works_Fine()
        {
            // arrange            
            subject.AddUrl(CreateNewUrlModel("https://www.bbc.co.uk", "https://localhost/abc", "abc"));
            subject.AddUrl(CreateNewUrlModel("https://www.bbc.co.uk/sports", "https://localhost/abcd", "abcd"));

            // act
            var result = subject.GetUrls();

            // assert
            Assert.AreEqual(2, result.Count());
        }

        private UrlModel CreateNewUrlModel(string original, string shortened, string code)
        {
            return new UrlModel
            {
                OriginalUrl = original,
                ShortenedUrl = shortened,
                Code = code
            };
        }
    }
}
