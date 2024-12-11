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
            return new ShelterDTO
            {
                City = shelter.City,
                Name = shelter.Name,
                Address = shelter.Address,
                Capacity = shelter.Capacity,
                AvailableSpaces = shelter.AvailableSpaces,
                PhoneNumber = shelter.PhoneNumber
            };
        }

        public static Shelter updateShelter(this Shelter shelter,ShelterDTO shelterDTO)
        {
            shelter.City = shelterDTO.City;
            shelter.Name = shelterDTO.Name;
            shelter.Address = shelterDTO.Address;
            shelter.Capacity = shelterDTO.Capacity;
            shelter.AvailableSpaces = shelterDTO.AvailableSpaces;
            shelter.PhoneNumber = shelterDTO.PhoneNumber;
            return shelter;            
        }
    }
}
