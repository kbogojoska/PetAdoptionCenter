using PetShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.DTO
{
    public class RequestPetDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Weight { get; set; }
        public SizeOfAnimal Size { get; set; }
        [Range(0,100)]
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public string? Breed { get; set; }
        public string? About { get; set; }
        public AnimalType Type { get; set; }
        public string? HealthInformation { get; set; }
        public string? ImageURL { get; set; }
        public double PriceForAdoption { get; set; }
        public bool isAvailable { get; set; }
        public Guid ShelterOfResidenceId { get; set; }

    }
}
