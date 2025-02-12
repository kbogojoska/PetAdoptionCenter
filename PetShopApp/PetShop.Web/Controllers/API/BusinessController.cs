using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.Service.Interface;

namespace PetShop.Web.Controllers.API
{
    [Route("api/business")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpPost("transfer")]
        public IActionResult TransferData()
        {
            _businessService.TransferBusinessData();
            return Ok("Data transferred successfully.");
        }
    }
}
