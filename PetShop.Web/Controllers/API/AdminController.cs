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

		[HttpGet("AdoptionApplication/{id}")]
		public AdminResponseAdoptionApplicationDTO GetAdoptionApplication(String? id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException(nameof(id), "ID cannot be null or empty");
			}
			return _adminService.FindById(id);
		}

	}
}
