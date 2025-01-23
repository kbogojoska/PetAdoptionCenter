using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.DTO
{
    public class PetShopApplicationUserDTO
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public string? ContactPhoneNumber { get; set; }
        public string? Address { get; set; }

        public List<AdoptionApplication>? AdoptionApplications { get; set; }
    }
}
