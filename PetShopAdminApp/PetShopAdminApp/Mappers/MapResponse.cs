using PetShopAdminApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetShopAdminApp.Mappers
{
	public static class MapResponse
	{
		public static AdoptionApplication MapResponseToApplication(this ResponseAdoptionApplication data)
		{
			if (data == null) throw new ArgumentNullException(nameof(data));

			data.Pet.Id = data.PetId;

			var pet = data.Pet;
			var user = new PetShopUser
			{
				Id = data.ApplicantId,
				Address = data.ApplicantAddress,
				ContactPhoneNumber = data.ApplicantContactPhoneNumber,
				Age = data.ApplicantAge,
				Email = data.ApplicantEmail,
				Name = data.ApplicantName,
				Surname = data.ApplicantSurname,
			};
			var shelter = new Shelter
			{
				Id = data.ShelterId,
				Address = data.ShelterAddress,
				CityOfOperation = data.ShelterCityOfOperation,
				Name = data.ShelterName,
				PhoneNumber = data.ShelterPhoneNumber,
			};

			if (user == null || shelter == null || pet == null)
			{
				throw new NullReferenceException("Object cannot be null");
			}

			return new AdoptionApplication
			{
				Id = data.Id,
				Pet = pet,
				Shelter = shelter,
				User = user,
				IsValid = data.IsValid,
				ApplicationDate = data.ApplicationDate,
				SumOfAdoptionFee = data.SumOfAdoptionFee,
			};
		}
	}
}
