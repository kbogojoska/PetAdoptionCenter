using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.DTO
{
    public class ShelterDTO
    {
        public string? City { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int Capacity { get; set; }
        public int AvailableSpaces { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid Id { get; set; } // Shelter ID
        public List<Pet>? Pets { get; set; } = new List<Pet>();

    }
}
