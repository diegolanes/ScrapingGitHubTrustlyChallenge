using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ScrapingGitHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return @"To hit the endpoint use the url: https://localhost:44339/api/scrap/{user}/{repositorio}";
        }
        [HttpGet("{user}/{repo}")]
        public ActionResult<string> Get(string user, string repo)
        {            
            return "value";
        }
    }
}