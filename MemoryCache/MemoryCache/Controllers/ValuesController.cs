using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MemoryCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache){
            _memoryCache = memoryCache;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var cacheKey = "TheTime";
            DateTime existingTime;
            if (_memoryCache.TryGetValue(cacheKey, out existingTime))
            {
                return "Fetch from cache: " + existingTime.ToString();
            }
            else
            {
                existingTime = DateTime.UtcNow;
                _memoryCache.Set(cacheKey, existingTime);
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
