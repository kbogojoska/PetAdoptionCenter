using PetShopAdminApp.Enum;

namespace PetShopAdminApp.Models
{
	public class Pet
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public double Weight { get; set; }
		public SizeOfAnimal Size { get; set; }
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
