using PetShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities
{
    public class AdoptionApplication : BaseEntity
    {
        [Required]
        public required PetShopApplicationUser Applicant { get; set; }
        [Required]
        public Guid PetId { get; set; }
        [Required]
        public required Pet Pet { get; set; }
        public bool isValid { get; set; } = false;
        [Required]
        public DateTime ApplicationDate = DateTime.UtcNow;
        [Required]
        public double sumOfAdoptionFee { get; set; }
        // public string? DocumentPath { get; set; }

        public bool IsValidApplication()
        {
            return Applicant.Age >= 18 && Pet.isAvailable;
        }

        public override string? ToString()
        {
            return $"Application ID: {Id}, Applicant: {Applicant.Name} {Applicant.Surname}, Pet: {Pet.Name}, Fee: {sumOfAdoptionFee:C}, Date: {ApplicationDate}";
        }

    }
}
