using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenDemo.Filters;
using TokenDemo.Untity;

namespace TokenDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ICustomJWTService _jwtservice;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jWTService"></param>
        public AuthenticationController(ICustomJWTService jWTService)
        {
            _jwtservice = jWTService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        [TestAction]  方法过滤
        public IActionResult Login(string name,string pwd)
        {
            if(name=="one"&& pwd == "789456")
            {
               string token = _jwtservice.GetJWTToken(name, pwd);
                return Ok(new { res = true, token });
            }
            else
            {
                return BadRequest(new { res = false, token ="" });
            }
            
        }
    }
}
