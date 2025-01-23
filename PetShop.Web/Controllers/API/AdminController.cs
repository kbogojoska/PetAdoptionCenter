using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using PetShop.Service.Interface;
using PetShop.Service.Mappers;

namespace PetShop.Web.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IAdminService _adminService;

		public AdminController(IAdminService adminService)
		{
			_adminService = adminService;
		}

		[HttpGet]
		[HttpGet("All")]
		public List<AdminResponseAdoptionApplicationDTO> GetAllAdoptionApplications()
		{
			return _adminService.FindAll();
		}

		[HttpPost("[action]")]
		public AdminResponseAdoptionApplicationDTO GetAdoptionApplication(BaseEntity model)
		{
			if (model == null || model.Id == null)
			{
				throw new ArgumentNullException("ID or model cannot be null or empty");
			}
			return _adminService.FindById(model.Id.ToString());
		}

	}
}
