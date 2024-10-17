using Microsoft.AspNetCore.Mvc;
using NetCore.MiddleWares;
using NetCore.Models;

namespace NetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Handle([FromBody]UserInfo userInfo)
        {
            var token = TokenProvider.Create(userInfo);

            userInfo.token = token;

            return Ok(userInfo);
        }


    }
}
