using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using UrlShorteningApp.Services;

namespace UrlShorteningApp.Tests
{
    [TestFixture]
    public class UrlServiceTests
    {
        private IUrlService subject;

        [SetUp]
        public void Setup()
        {   
            subject = new UrlService(new NullLogger<UrlService>());
        }

        [Test]
        public void Generate_Code_By_Url()
        {
            // arrange
            var url = "www.bbc.com";

            // act
            var result = subject.GenerateCode(url);
            
            // assert
            Assert.IsAssignableFrom<string>(result);
        }

        [Test]
        public void Parse_InValid_Url()
        {
            // arrange
            var url = "www.bbc.com";

            // act
            var result = subject.Parse(url);

            // assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Parse_Valid_Url()
        {
            // arrange
            var url = "https://www.bbc.com";

            // act
            var result = subject.Parse(url);

            // assert
            Assert.IsTrue(result);
        }
    }
}