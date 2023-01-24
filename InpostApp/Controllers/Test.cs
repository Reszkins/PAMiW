using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InpostApp.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class Test : ControllerBase
    {
        [HttpGet]
        [Route("gettest")]
        [Authorize]
        //%[Authorize("read:current_user")]
        //[EnableCors]
        public IActionResult GetTest()
        {
            var xd = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok("Działa do kurwy jebanej " + xd);
        }
    }
}
