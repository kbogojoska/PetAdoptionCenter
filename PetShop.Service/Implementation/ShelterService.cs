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
    public class ShelterService : IShelterService  // add validation
    {
        protected readonly IRepository<Shelter> shelterRepository;

        public ShelterService(IRepository<Shelter> shelterRepository)
        {
            this.shelterRepository = shelterRepository;
        }

        public ShelterDTO DeleteById(string id)
        {
            Shelter shelter = shelterRepository.Delete(Guid.Parse(id));
            return shelter.toShelterDTO();
        }

        public List<ShelterDTO> FindAll()
        {
            return shelterRepository.GetAll()
                .Select(item => item.toShelterDTO())
                .ToList();
        }

        public ShelterDTO FindById(string id)
        {
            Guid guidId = Guid.Parse(id);
            return shelterRepository.Get(guidId).toShelterDTO();
        }

        public ShelterDTO Store(ShelterDTO tDto)
        {
            Shelter shelter = new Shelter
            {
                City = tDto.City,
                Name = tDto.Name,
                Address = tDto.Address,
                Capacity = tDto.Capacity,
                AvailableSpaces = tDto.AvailableSpaces,
                PhoneNumber = tDto.PhoneNumber
            };
            shelterRepository.Insert(shelter);
            return shelter.toShelterDTO();
        }

        public ShelterDTO Update(string id, ShelterDTO shelterDTO)
        {
            Shelter shelter = shelterRepository.Get(Guid.Parse(id));
            shelter.updateShelter(shelterDTO);
            shelterRepository.Update(shelter);
            return shelter.toShelterDTO();
        }
    }
}
