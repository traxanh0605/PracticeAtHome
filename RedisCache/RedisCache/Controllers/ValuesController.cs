using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDistributedCache _distributeCache;

        public ValuesController(IDistributedCache distributedCache)
        {
            _distributeCache = distributedCache;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var cacheKey = "TheTime";
            var existingTime = _distributeCache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(existingTime))
            {
                return "Fetch from cache : " + existingTime;
            }
            else
            {
                existingTime = DateTime.UtcNow.ToString();
                _distributeCache.SetString(cacheKey, existingTime);
                return "Added to cache : " + existingTime;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
