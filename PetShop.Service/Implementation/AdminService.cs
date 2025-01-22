using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using PetShop.Service.Implementation.PetShop.Service;
using PetShop.Service.Interface;
using PetShop.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Implementation
{
	public class AdminService : IAdminService
	{
		private readonly IAdoptionApplicationService adoptionApplicationService;
		private readonly IUserService userService;
		private readonly IPetService petService;

		public AdminService(IAdoptionApplicationService adoptionApplicationService, IUserService userService, IPetService petService)
		{
			this.adoptionApplicationService = adoptionApplicationService;
			this.userService = userService;
			this.petService = petService;
		}

		public List<AdminResponseAdoptionApplicationDTO> FindAll()
		{
			return adoptionApplicationService.FindAll().Select(s =>
			{
				var pet = petService.FindById(s.PetId).toResponsePetDto();
				var user = userService.FindById(s.ApplicantId);
				return new AdminResponseAdoptionApplicationDTO
				{
					Id = s.Id,
					PetId = s.PetId,
					Pet = pet,
					ApplicantId = s.ApplicantId,
					ApplicantName = user.Name,
					ApplicantSurname = user.Surname,
					ApplicantAge = user.Age,
					ApplicantEmail = user.Email,
					ApplicantAddress = user.Address,
					ApplicantContactPhoneNumber = user.ContactPhoneNumber,
					IsValid = s.IsValid,
					ApplicationDate = s.ApplicationDate,
					SumOfAdoptionFee = s.SumOfAdoptionFee

				};
				
			}).ToList();
		}

		public AdminResponseAdoptionApplicationDTO FindById(string? id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException(nameof(id), "ID cannot be null or empty");
			}

			AdoptionApplicationDTO adoptionApplication = adoptionApplicationService.FindById(id);

			if (adoptionApplication == null)
			{
				throw new NullReferenceException("Object cannot be null");
			}

			var pet = petService.FindById(adoptionApplication.PetId).toResponsePetDto();
			var user = userService.FindById(adoptionApplication.ApplicantId);
			return new AdminResponseAdoptionApplicationDTO
			{
				Id = adoptionApplication.Id,
				PetId = adoptionApplication.PetId,
				Pet = pet,
				ApplicantId = adoptionApplication.ApplicantId,
				ApplicantName = user.Name,
				ApplicantSurname = user.Surname,
				ApplicantAge = user.Age,
				ApplicantEmail = user.Email,
				ApplicantAddress = user.Address,
				ApplicantContactPhoneNumber = user.ContactPhoneNumber,
				IsValid = adoptionApplication.IsValid,
				ApplicationDate = adoptionApplication.ApplicationDate,
				SumOfAdoptionFee = adoptionApplication.SumOfAdoptionFee

			};
		}
	}
}
