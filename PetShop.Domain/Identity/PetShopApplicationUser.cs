using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PetShop.Domain.Identity
{
    public class PetShopApplicationUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string? ContactPhoneNumber { get; set; }

        [Required]
        public string? CityOfResidence { get; set; }

        [Required]
        public string? Address { get; set; }
    }
}
