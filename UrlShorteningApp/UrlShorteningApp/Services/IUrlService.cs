namespace UrlShorteningApp.Services
{
    public interface IUrlService
    {
        string GenerateCode(string url);
        bool Parse(string url);
    }
}
