using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using PetShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Mappers
{
    public static class PetMapper
    {
        public static PetDTO toPetDTO(this Pet pet)
        {
            return new PetDTO
            {
                Name = pet.Name,
                Weight = pet.Weight,
                Size = pet.Size ?? SizeOfAnimal.UNDISCLOSED,
                Age = pet.Age,
                Gender = pet.Gender,
                Breed = pet.Breed ?? "",
                About = pet.About ?? "",
                Type = pet.Type,
                HealthInformation = pet.HealthInformation ?? "",
                ImageURL = pet.ImageURL,
                PriceForAdoption = pet.PriceForAdoption,
                isAvailable = pet.isAvailable,
                ShelterOfResidenceId = pet.ShelterOfResidence.Id
            };
        }
    }
}
