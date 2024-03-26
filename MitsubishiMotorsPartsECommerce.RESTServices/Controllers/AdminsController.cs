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
    public class AdminsController : ControllerBase
    {
        private readonly IAdminBLL _adminBLL;
        private readonly AppSettings _appSettings;
        private readonly ILogger<AdminsController> _logger;

        public AdminsController(IAdminBLL adminBLL, IOptions<AppSettings> appSettings, ILogger<AdminsController> logger)
        {
            _adminBLL = adminBLL;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        [Authorize(Roles = "superadmin")]
        [HttpGet]
        public async Task<IEnumerable<AdminDTO>> Get()
        {
            var admins = _adminBLL.GetAll();
            return admins;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var admin = _adminBLL.Login(loginDTO.Username, loginDTO.Password);
                if (admin == null)
                {
                    return Unauthorized();
                }

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, loginDTO.Username));
                claims.Add(new Claim(ClaimTypes.Role, admin.Role.Trim()));
                var tokenHandler = new JwtSecurityTokenHandler();

                //_logger.LogInformation($"---------------------------> {_appSettings.Secret}");
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

                //_logger.LogInformation($"---------------------------> Token generated for {tokenHandler.WriteToken(token)}");
                var tokenDto = new TokenDTO
                {
                    Username = loginDTO.Username,
                    Token = tokenHandler.WriteToken(token)
                };
                return Ok(tokenDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [Authorize(Roles = "superadmin")]
        [HttpGet("{id}")]
        public async Task<AdminDTO> Get(string adminname)
        {
            var admin = _adminBLL.GetByUsername(adminname);
            return admin;
        }



        [Authorize(Roles = "superadmin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdminCreateDTO adminCreateDTO)
        {
            try
            {
                _adminBLL.Insert(adminCreateDTO);
                return CreatedAtAction("Get", adminCreateDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "superadmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string username)
        {
            try
            {
                _adminBLL.Delete(username);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
