using Microsoft.IdentityModel.Tokens;
using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Mappers
{
    public static class ShelterMapper
    {
        public static ShelterDTO toShelterDTO(this Shelter shelter)
        {
            if (shelter == null)
            {
                throw new ArgumentNullException(nameof(shelter), "Shelter cannot be null");
            }

			var pets = new List<Pet>();

			if (!shelter.Pets.IsNullOrEmpty())
            {
				pets = shelter.Pets.Select(p => p).ToList();
			}


			return new ShelterDTO
            {
                Id = shelter.Id,
                City = shelter.City,
                Name = shelter.Name,
                Address = shelter.Address,
                Capacity = shelter.Capacity,
                AvailableSpaces = shelter.AvailableSpaces,
                PhoneNumber = shelter.PhoneNumber,
                Pets = pets
            };
        }

        public static Shelter updateShelter(this Shelter shelter,ShelterDTO shelterDTO)
        {
            if (shelter == null)
            {
                throw new ArgumentNullException(nameof(shelter), "Shelter cannot be null");
            }
            if (shelterDTO == null)
            {
                throw new ArgumentNullException(nameof(shelterDTO), "ShelterDTO cannot be null");
            }

            shelter.City = shelterDTO.City;
            shelter.Name = shelterDTO.Name;
            shelter.Address = shelterDTO.Address;
            shelter.Capacity = shelterDTO.Capacity;
            shelter.AvailableSpaces = shelterDTO.AvailableSpaces;
            shelter.PhoneNumber = shelterDTO.PhoneNumber;
            shelter.Pets = shelterDTO.Pets.Select(p => p).ToList();
            return shelter;            
        }

        public static Shelter toShelter(this ShelterDTO shelterDto)
        {
            if (shelterDto == null)
            {
                throw new ArgumentNullException(nameof(shelterDto), "ShelterDTO cannot be null");
            }

            var shelter = new Shelter
            {
                Id = shelterDto.Id,
                City = shelterDto.City,
                Name = shelterDto.Name,
                Address = shelterDto.Address,
                Capacity = shelterDto.Capacity,
                AvailableSpaces = shelterDto.AvailableSpaces,
                PhoneNumber = shelterDto.PhoneNumber,
            };

            shelter.Pets = shelterDto.Pets?.Select(p => p).ToList();

            return shelter;
        }
    }
}
