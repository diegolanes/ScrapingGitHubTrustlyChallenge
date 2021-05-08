using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ScrapingGitHubAPI.Domain;

namespace ScrapingGitHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ScrapController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET api/scrap
        [HttpGet]
        public ActionResult<string> Get()
        {
            return @"
            To hit the endpoint use the url: 

            {baseUrlAPI}/api/scrap/{user}/{repositorio}
                
            Example: 

            https://localhost:44339/api/scrap/amfe/slider-js";
        }
        // GET api/scrap/user/repo
        [HttpGet("{user}/{repo}")]
        public ActionResult<List<ItemDescriptionResult>> Get(string user, string repo)
        {
            ConcurrentBag<ItemDescription> itemsDescription = new ConcurrentBag<ItemDescription>();
            var baseUrl = ScrapUtils.getBaseUrlGitHub(_configuration);
            Scrap.getUrlContent(baseUrl, $"/{user}/{repo}", itemsDescription);
            return Scrap.getResultList(itemsDescription.ToList());
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