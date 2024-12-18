using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities
{
    public class Shelter : BaseEntity
    {
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public int AvailableSpaces { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public virtual ICollection<Pet>? Pets { get; set; }
    }
}
