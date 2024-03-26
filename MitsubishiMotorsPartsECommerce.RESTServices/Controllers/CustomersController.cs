using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.BLL.Interfaces;
using MitsubishiMotorsPartsECommerce.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MitsubishiMotorsPartsECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerBLL _customerBLL;
        private readonly AppSettings _appSettings;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerBLL customerBLL, IOptions<AppSettings> appSettings, ILogger<CustomersController> logger)
        {
            _customerBLL = customerBLL;
            _appSettings = appSettings.Value;
            _logger = logger;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CustomerLoginDTO customerLoginDto)
        {
            try
            {
                var customer = _customerBLL.Login(customerLoginDto);
                if (customer == null)
                {
                    return Unauthorized();
                }

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("CustomerID", customer.CustomerID.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, customer.FirstName));
                claims.Add(new Claim(ClaimTypes.Role, "customer"));
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var tokenDto = new TokenDTO
                {
                    Username = customer.FirstName,
                    Token = tokenHandler.WriteToken(token)
                };
                return Ok(tokenDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
