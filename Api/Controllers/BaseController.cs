using System.Configuration;
using System.Runtime.Caching;
using System.Web.Http;

namespace Api.Controllers
{
    public class BaseController : ApiController
    {
        public MemoryCache memoryCache = MemoryCache.Default;
        private static string LoginToken = ConfigurationManager.AppSettings["Token"];

        public bool Login(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            return string.Equals(token, LoginToken, System.StringComparison.OrdinalIgnoreCase);
        } 
    }
}