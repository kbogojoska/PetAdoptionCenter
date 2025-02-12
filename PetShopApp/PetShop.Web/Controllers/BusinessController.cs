using Microsoft.AspNetCore.Mvc;
using PetShop.Service.Implementation;
using PetShop.Service.Interface;

namespace PetShop.Web.Controllers
{
    public class BusinessController : Controller
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        public async Task<IActionResult> Index()
        {
            var businesses = _businessService.GetAllBusinesses();
            return View(businesses);
        }
    }
}
