using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
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
                ApplicantId = adoptionApplication.Applicant.Id,
                PetId = adoptionApplication.Pet.Id,
                isValid = adoptionApplication.isValid,
                ApplicationDate = adoptionApplication.ApplicationDate,
                sumOfAdoptionFee = adoptionApplication.sumOfAdoptionFee
            };
        }
    }
}
