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
    public class SalesOrdersController : ControllerBase
    {
        private readonly ISalesOrderBLL _salesorderBLL;
        private readonly AppSettings _appSettings;
        private readonly ILogger<SalesOrdersController> _logger;

        public SalesOrdersController(ISalesOrderBLL salesorderBLL, IOptions<AppSettings> appSettings, ILogger<SalesOrdersController> logger)
        {
            _salesorderBLL = salesorderBLL;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        [Authorize(Roles = "customer")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int customerID;
            if (User.FindFirstValue("CustomerID") != null)
            {
                customerID = int.Parse((User.FindFirstValue("CustomerID")));
            } else
            {
                return Unauthorized();
            }
            var salesorder = _salesorderBLL.GetSalesOrderHeaderByCustomerID(customerID);
            return Ok(salesorder);
        }



        [Authorize(Roles = "customer")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string lstProd)
        {
            
            try
            {
                int customerID;
                if (User.FindFirstValue("CustomerID") != null)
                {
                    customerID = int.Parse((User.FindFirstValue("CustomerID")));
                }
                else
                {
                    return Unauthorized();
                }
                var salesorderCreateDTO = new SalesOrderCreateDTO
                {
                    CustomerID = customerID,
                    LstProd = lstProd
                };
                _salesorderBLL.Create(salesorderCreateDTO);
                return CreatedAtAction("Get", salesorderCreateDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
