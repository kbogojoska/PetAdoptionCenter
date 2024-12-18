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
        public static ResponsePetDTO toResponsePetDto(this Pet pet)
        {
            return new ResponsePetDTO
            {
                Name = pet.Name,
                Weight = pet.Weight,
                Size = pet.Size,
                Age = pet.Age,
                Gender = pet.Gender,
                Breed = pet.Breed ?? "",
                About = pet.About ?? "",
                Type = pet.Type,
                HealthInformation = pet.HealthInformation ?? "",
                ImageURL = pet.ImageURL,
                PriceForAdoption = pet.PriceForAdoption,
                isAvailable = pet.isAvailable,
                ShelterOfResidenceId = pet.ShelterOfResidence?.Id ?? Guid.Empty
            };
        }

        public static RequestPetDTO toRequestPetDto(this Pet pet)
        {
            return new RequestPetDTO
            {
                Id = pet.Id,
                Name = pet.Name,
                Weight = pet.Weight,
                Size = pet.Size,
                Age = pet.Age,
                Gender = pet.Gender,
                Breed = pet.Breed,
                About = pet.About,
                Type = pet.Type,
                HealthInformation = pet.HealthInformation,
                ImageURL = pet.ImageURL,
                PriceForAdoption = pet.PriceForAdoption,
                isAvailable = pet.isAvailable,
                ShelterOfResidenceId = pet.ShelterOfResidence?.Id ?? Guid.Empty
            };
        }
    }
}
