using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    public class DinnersController: ApiController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<string> { "Dinner 1", "Dinner 2" });
        }
    }
}