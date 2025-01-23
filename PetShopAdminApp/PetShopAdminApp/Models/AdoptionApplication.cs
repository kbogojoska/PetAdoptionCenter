namespace PetShopAdminApp.Models
{
	public class AdoptionApplication
	{
		public Guid Id { get; set; }
		public Pet Pet { get; set; }
		public Shelter Shelter { get; set; }
		public PetShopUser User { get; set; }
		public bool IsValid { get; set; } = false;
		public DateTime ApplicationDate { get; set; }
		public double SumOfAdoptionFee { get; set; }
	}
}
