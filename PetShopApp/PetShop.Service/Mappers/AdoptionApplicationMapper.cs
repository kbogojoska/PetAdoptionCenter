using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using PetShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Mappers
{
    public static class AdoptionApplicationMapper
    {
        public static AdoptionApplicationDTO toDTO (this AdoptionApplication adoptionApplication)
        {
            return new AdoptionApplicationDTO
            {
                Id = adoptionApplication.Id,
                ApplicantId = adoptionApplication.ApplicantId,
                PetId = adoptionApplication.PetId,
                IsValid = adoptionApplication.IsValid,
                ApplicationDate = adoptionApplication.ApplicationDate,
                SumOfAdoptionFee = adoptionApplication.SumOfAdoptionFee
            };
        }

        public static AdoptionApplication updateAdopApp(this AdoptionApplication application, AdoptionApplicationDTO adoptionApplicationDTO, PetShopApplicationUser user, Pet pet)
        {
            application.Applicant = user;
            application.Pet = pet;
            application.PetId = adoptionApplicationDTO.PetId;
            application.IsValid = adoptionApplicationDTO.IsValid;
            application.ApplicationDate = adoptionApplicationDTO.ApplicationDate;
            application.SumOfAdoptionFee = adoptionApplicationDTO.SumOfAdoptionFee;

            return application;
        }

        public static AdoptionApplication toAdopApp(this AdoptionApplicationDTO adoptionApplicationDTO, PetShopApplicationUser applicant, Pet pet)
        {
            return new AdoptionApplication
            {
                ApplicantId = adoptionApplicationDTO.ApplicantId,
                Applicant = applicant,
                PetId = adoptionApplicationDTO.PetId,
                Pet = pet,
                IsValid = adoptionApplicationDTO.IsValid,
                ApplicationDate = adoptionApplicationDTO.ApplicationDate,
                SumOfAdoptionFee = adoptionApplicationDTO.SumOfAdoptionFee
            };
        }
    }
}
