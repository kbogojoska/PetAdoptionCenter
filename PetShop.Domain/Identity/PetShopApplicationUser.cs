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
        public string? Name;
        [Required]
        public string? Surname;
        [Required]
        public int Age;
        [Required]
        public string? ContactPhoneNumber;
    }
}
