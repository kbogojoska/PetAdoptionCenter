namespace PetShopAdminApp.Models
{
	public class ResponseAdoptionApplication
	{
		public Guid Id { get; set; }
		public Guid PetId { get; set; }
		public Pet Pet { get; set; }
		public Guid ShelterId { get; set; }
		public string ShelterCityOfOperation { get; set; }
		public string ShelterName { get; set; }
		public string ShelterAddress { get; set; }
		public string ShelterPhoneNumber { get; set; }
		public string ApplicantId { get; set; }
		public string ApplicantName { get; set; }
		public string ApplicantSurname { get; set; }
		public int ApplicantAge { get; set; }
		public string ApplicantContactPhoneNumber { get; set; }
		public string ApplicantEmail { get; set; }
		public string ApplicantAddress { get; set; }
		public bool IsValid { get; set; } = false;
		public DateTime ApplicationDate { get; set; }
		public double SumOfAdoptionFee { get; set; }
	}
}
