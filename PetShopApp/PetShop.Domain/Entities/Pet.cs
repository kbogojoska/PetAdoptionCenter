using PetShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities
{
    public class Pet : BaseEntity
    {
        [Required]
        public string? Name {  get; set; }
        [Required]
        public double Weight { get; set; }
        public SizeOfAnimal Size {  get; set; }
        [Required]
		[Range(0, 100)]
		public int Age {  get; set; }
        [Required]
        public GenderType Gender {  get; set; }
        public string? Breed {  get; set; }
        public string? About {  get; set; }
        [Required]
        public AnimalType Type {  get; set; }
        public string? HealthInformation {  get; set; }
        [Required]
        public string? ImageURL {  get; set; }
        [Required]
        public double PriceForAdoption {  get; set; }
        [Required]
        public bool isAvailable { get; set; }
        [Required]
        public Guid ShelterOfResidenceId { get; set; }
        
        public Shelter? ShelterOfResidence {  get; set; }

        public override string? ToString()
        {
            return "Pet Name: " + Name + ", gender of animal: " + Gender + ", age: " + Age + "and price: " + PriceForAdoption + "\n";
        }
    }
}
