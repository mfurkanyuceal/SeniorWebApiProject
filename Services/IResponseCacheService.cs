using System;
using System.Threading.Tasks;

namespace SeniorWepApiProject.Services
{
    public interface IResponseCacheService
    {
        public interface IResponseCacheService
        {
            Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeTimeLive);

            Task<string> GetCachedResponseAsync(string cacheKey);
        }
    }
}