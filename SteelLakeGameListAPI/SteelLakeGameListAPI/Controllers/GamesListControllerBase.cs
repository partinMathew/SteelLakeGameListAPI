using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Controllers
{
    public class GamesListControllerBase : ControllerBase
    {

        private readonly IDistributedCache _distributedCache;

        public GamesListControllerBase(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public async Task CacheResponse(string key, int seconds, object response)
        {
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddSeconds(seconds));
            var serializedReponse = JsonConvert.SerializeObject(response);
            await _distributedCache.SetStringAsync(key, serializedReponse, options);
        }
    }
}
