using PetShop.Domain.Entities;
using PetShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.DTO
{
    public class AdoptionApplicationDTO
    {
        public string? ApplicantId { get; set; }
        public Guid PetId { get; set; }
        public bool IsValid { get; set; } = false;
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public double SumOfAdoptionFee { get; set; }
    }
}
