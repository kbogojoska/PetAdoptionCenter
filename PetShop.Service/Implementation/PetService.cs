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
            var shelter = shelterRepository.Get(FindById(id).ShelterOfResidenceId) ?? throw new Exception("Shelter not found.");
            shelter.AvailableSpaces = shelter.AvailableSpaces + 1;
            Pet pet = petRepository.Delete(id);

            return pet.toResponsePetDto();
        }

        public List<RequestPetDTO> FindAll(bool? isAvailable=null)
        {
            var pets = petRepository.GetAll()
                                .Select(pet => pet.toRequestPetDto())
                                .ToList();
            if(isAvailable == true)
            {
                return pets.Where(p => p.isAvailable== true).ToList();
            }

            return pets;
        }


        public RequestPetDTO FindById(Guid id)
        {
            var pet = petRepository.Get(id) ?? throw new Exception("Pet not found.");
            return pet.toRequestPetDto();
        }

        public List<RequestPetDTO> FindByShelter(Guid shelterId, bool? isAvailable)
        {
            if (shelterId == Guid.Empty)
            {
                throw new ArgumentException("Invalid shelter ID.");
            }

            var pets = petRepository.FindBy(p => p.ShelterOfResidenceId == shelterId);

			if (isAvailable == true)
			{
				pets = pets.Where(p => p.isAvailable == true);
			}

			return pets.Select(pet => pet.toRequestPetDto()).ToList();
        }

        public List<RequestPetDTO> FindByCity(string city, bool? isAvailable)
        {
            if (city == null)
            {
                throw new ArgumentException("Invalid search by city.");
            }

            var petsToReturn = petRepository.GetAll().Where(p => p.ShelterOfResidence.City == city);

			if (isAvailable == true)
			{
				petsToReturn = petsToReturn.Where(p => p.isAvailable == true);
			}

			return petsToReturn.Select(pet => pet.toRequestPetDto()).ToList();

        }

        public ResponsePetDTO Store(RequestPetDTO requestPetDto) 
        {
            var shelter = shelterRepository.Get(requestPetDto.ShelterOfResidenceId) ?? throw new Exception("Shelter not found.");
            Pet pet = requestPetDto.ToPet(shelter);
            shelter.AvailableSpaces = shelter.AvailableSpaces - 1;
			if (shelter.Pets == null)
			{
				shelter.Pets = new HashSet<Pet>();
			}
			shelter.Pets.Add(pet);
            petRepository.Insert(pet);

            return pet.toResponsePetDto();
        }



        public ResponsePetDTO Update(Guid id, RequestPetDTO requestPetDto)
        {

            Pet pet = petRepository.Get(id);
            if (pet == null) throw new Exception("Pet not found.");

            var shelter = shelterRepository.Get(requestPetDto.ShelterOfResidenceId);
            if (shelter == null) throw new Exception("Shelter not found.");

            pet.UpdateFromRequestDto(requestPetDto, shelter);
            petRepository.Update(pet);

            return pet.toResponsePetDto();
        }


    }
}
