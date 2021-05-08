using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            return @"To hit the endpoint use the url: https://localhost:44339/api/scrap/{user}/{repositorio}";
        }
        // GET api/scrap/user/repo
        [HttpGet("{user}/{repo}")]
        public ActionResult<List<ItemDescriptionResult>> Get(string user, string repo)
        {
            List<ItemDescription> itemsDescription = new List<ItemDescription>();
            var baseUrl = Utils.getBaseUrlGitHub(_configuration);
            Scrap.start($"{baseUrl}/{user}/{repo}", ref itemsDescription);
            return Scrap.getResultList(itemsDescription);
        }
    }
}