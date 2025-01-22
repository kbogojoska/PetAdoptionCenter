using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.DTO
{
	public class AdminResponseAdoptionApplicationDTO
	{
		public Guid Id { get; set; }
		public Guid PetId { get; set; }
		public ResponsePetDTO Pet { get; set; }
		public string ApplicantId { get; set; }
		public string ApplicantName { get; set; }
		public string ApplicantSurname { get; set;}
		public int ApplicantAge { get; set; }
		public string ApplicantContactPhoneNumber {  get; set; }
		public string ApplicantEmail { get; set; }
		public string ApplicantAddress { get; set; }
		public bool IsValid { get; set; } = false;
		public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
		public double SumOfAdoptionFee { get; set; }
	}
}
