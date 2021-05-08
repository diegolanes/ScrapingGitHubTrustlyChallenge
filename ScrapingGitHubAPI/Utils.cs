using Microsoft.Extensions.Configuration;

namespace ScrapingGitHubAPI
{
    public class Utils
    {
        public const int kbMultiplier = 1000;
        public const int mbMultiplier = 1000000;
        public const int gbMultiplier = 1000000000;

        public static string getBaseUrlGitHub(IConfiguration configuration)
        {
            return configuration.GetSection("BaseUrlGitHub").Value;
        }
    }
}
