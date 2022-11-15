using Microsoft.AspNetCore.Mvc;

namespace MiddlewareDemo.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet(Name = "GetError")]
        public IActionResult GetError()
        {
            throw new Exception();
            //return Ok();
        }
    }
}
