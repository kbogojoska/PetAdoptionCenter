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
                ShelterOfResidenceId = pet.ShelterOfResidenceId
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
                ShelterOfResidenceId = pet.ShelterOfResidenceId
            };
        }

        public static void UpdateFromRequestDto(this Pet pet, RequestPetDTO requestDto, Shelter shelter)
        {
            pet.Name = requestDto.Name;
            pet.Weight = requestDto.Weight;
            pet.Size = requestDto.Size;
            pet.Age = requestDto.Age;
            pet.Gender = requestDto.Gender;
            pet.Breed = requestDto.Breed;
            pet.About = requestDto.About;
            pet.Type = requestDto.Type;
            pet.HealthInformation = requestDto.HealthInformation;
            pet.ImageURL = requestDto.ImageURL;
            pet.PriceForAdoption = requestDto.PriceForAdoption;
            pet.isAvailable = requestDto.isAvailable;
            pet.ShelterOfResidence = shelter;
        }

        // for requestDTO
        public static Pet ToPet(this RequestPetDTO requestDto, Shelter shelter)
        {
            return new Pet
            {
                Name = requestDto.Name,
                Weight = requestDto.Weight,
                Size = requestDto.Size,
                Age = requestDto.Age,
                Gender = requestDto.Gender,
                Breed = requestDto.Breed,
                About = requestDto.About,
                Type = requestDto.Type,
                HealthInformation = requestDto.HealthInformation,
                ImageURL = requestDto.ImageURL,
                PriceForAdoption = requestDto.PriceForAdoption,
                isAvailable = requestDto.isAvailable,
                ShelterOfResidence = shelter,
                ShelterOfResidenceId = shelter.Id
            };
        }

        // for responseDTO
        public static Pet ToPet(this ResponsePetDTO dto, Shelter shelter)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "ResponsePetDTO cannot be null");
            }
            if (shelter == null)
            {
                throw new ArgumentNullException(nameof(shelter), "Shelter cannot be null");
            }

            return new Pet
            {
                Name = dto.Name,
                Weight = dto.Weight,
                Size = dto.Size,
                Age = dto.Age,
                Gender = dto.Gender,
                Breed = dto.Breed,
                About = dto.About,
                Type = dto.Type,
                HealthInformation = dto.HealthInformation,
                ImageURL = dto.ImageURL,
                PriceForAdoption = dto.PriceForAdoption,
                isAvailable = dto.isAvailable,
                ShelterOfResidence = shelter,
                ShelterOfResidenceId = shelter.Id 
            };
        }


    }
}
