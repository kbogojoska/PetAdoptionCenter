using Microsoft.EntityFrameworkCore;
using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using PetShop.Repository.Interface;
using PetShop.Service.Interface;
using PetShop.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Implementation
{
    public class PetService : IPetService
    {
        protected readonly IRepository<Pet> petRepository;
        protected readonly IRepository<Shelter> shelterRepository;

        public PetService(IRepository<Pet> petRepository, IRepository<Shelter> shelterRepository)
        {
            this.petRepository = petRepository;
            this.shelterRepository = shelterRepository;
        }
        public ResponsePetDTO DeleteById(Guid id)
        {
           Pet pet = petRepository.Delete(id);
           return pet.toResponsePetDto();
        }

        public List<RequestPetDTO> FindAll()
        {
            return petRepository.GetAll() // Get all Pet entities from the repository
        .Select(item => new RequestPetDTO
        {
            Id = item.Id,
            Name = item.Name,
            Weight = item.Weight,
            Size = item.Size,
            Age = item.Age,
            Gender = item.Gender,
            Breed = item.Breed,
            About = item.About,
            Type = item.Type,
            HealthInformation = item.HealthInformation,
            ImageURL = item.ImageURL,
            PriceForAdoption = item.PriceForAdoption,
            isAvailable = item.isAvailable,
            ShelterOfResidenceId = item.ShelterOfResidence?.Id ?? Guid.Empty
        })
        .ToList();
        }

        public RequestPetDTO  FindById(Guid id)
        {
            var pet = petRepository.Get(id);
            if (pet == null)
            {
                throw new Exception("Pet not found."); // Return null if the pet is not found
            }
            return pet.toRequestPetDto();
        }

        public ResponsePetDTO Store(RequestPetDTO requestPetDto)
        {
           

            var shelter = shelterRepository.Get(requestPetDto.ShelterOfResidenceId);
            if (shelter == null)
            {
                throw new Exception("Shelter not found.");
            }
            Pet pet = new Pet
            {
                Name = requestPetDto.Name,
                Weight = requestPetDto.Weight,
                Size = requestPetDto.Size,
                Age = requestPetDto.Age,
                Gender = requestPetDto.Gender,
                Breed = requestPetDto.Breed,
                About = requestPetDto.About,
                Type = requestPetDto.Type,
                HealthInformation = requestPetDto.HealthInformation,
                ImageURL = requestPetDto.ImageURL,
                PriceForAdoption = requestPetDto.PriceForAdoption,
                isAvailable = requestPetDto.isAvailable,
                ShelterOfResidence = shelter
            };

            petRepository.Insert(pet);
            return pet.toResponsePetDto();
        }

      

        public ResponsePetDTO Update(Guid id, RequestPetDTO requestPetDto)
        {

            Pet pet = petRepository.Get(id);
            if (pet == null) throw new Exception("Pet not found.");

            var shelter = shelterRepository.Get(requestPetDto.ShelterOfResidenceId);
            if (shelter == null) throw new Exception("Shelter not found.");

            pet.Name = requestPetDto.Name;
            pet.Weight = requestPetDto.Weight;
            pet.Size = requestPetDto.Size;
            pet.Age = requestPetDto.Age;
            pet.Gender = requestPetDto.Gender;
            pet.Breed = requestPetDto.Breed;
            pet.About = requestPetDto.About;
            pet.Type = requestPetDto.Type;
            pet.HealthInformation = requestPetDto.HealthInformation;
            pet.ImageURL = requestPetDto.ImageURL;
            pet.PriceForAdoption = requestPetDto.PriceForAdoption;
            pet.isAvailable = requestPetDto.isAvailable;
            pet.ShelterOfResidence = shelter;

            petRepository.Update(pet);

            return pet.toResponsePetDto();
        }

        
    }
}
