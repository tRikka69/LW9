using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace AmateurTheaterMongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult PublicEndpoint()
        {
            return Ok("Це відкритий ендпойнт. Вхід вільний.");
        }

        [Authorize]
        [HttpGet("private")]
        public IActionResult PrivateEndpoint()
        {
            var id = User.Claims.FirstOrDefault(c => 
                c.Type == "sub" || 
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;

            var email = User.Claims.FirstOrDefault(c => 
                c.Type == "email" || 
                c.Type == ClaimTypes.Email ||
                c.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value;

            var allClaimsDebug = User.Claims.Select(c => $"{c.Type} : {c.Value}").ToList();

            return Ok(new 
            { 
                Message = "Ви увійшли до VIP-ложі.", 
                UserId = id, 
                UserEmail = email,

                DebugClaims = allClaimsDebug 
            });
        }
    }
}