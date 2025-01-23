using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShopAdminApp.Mappers;
using PetShopAdminApp.Models;
using System;
using System.Text;

namespace PetShopAdminApp.Controllers
{
	public class AdoptionApplicationController : Controller
	{
        public AdoptionApplicationController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }


        public IActionResult Index()
		{
			HttpClient client = new HttpClient();

			string url = "https://localhost:44363/api/Admin/All";

			HttpResponseMessage response = client.GetAsync(url).Result;

			List<ResponseAdoptionApplication> data = response.Content.ReadAsAsync<List<ResponseAdoptionApplication>>().Result;

			List<AdoptionApplication> result = data.Select(s => s.MapResponseToApplication()).ToList();

			return View(result);
		}

		public IActionResult Details(string? id)
		{
			if (String.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("ID cannot be null or empty");
			}
			HttpClient client = new HttpClient();

			string url = "https://localhost:44363/api/Admin/GetAdoptionApplication";

			var model = new
			{
				Id = Guid.Parse(id),
			};

			HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

			HttpResponseMessage response = client.PostAsync(url, content).Result;

			ResponseAdoptionApplication data = response.Content.ReadAsAsync<ResponseAdoptionApplication>().Result;

			AdoptionApplication result = data.MapResponseToApplication();

			return View(result);
		}

        public FileContentResult CreateInvoice(string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("ID cannot be null or empty");
            }

            HttpClient client = new HttpClient();

            string url = "https://localhost:44363/api/Admin/GetAdoptionApplication";

            var model = new
            {
                Id = Guid.Parse(id),
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            ResponseAdoptionApplication data = response.Content.ReadAsAsync<ResponseAdoptionApplication>().Result;

            AdoptionApplication result = data.MapResponseToApplication();

			var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
			var document = DocumentModel.Load(templatePath);

			document.Content.Replace("{{isValid}}", result.IsValid ? "Valid" : "Invalid");
			document.Content.Replace("{{Id}}", result.Id.ToString());
			document.Content.Replace("{{ApplicantEmail}}", result.User.Email);
			document.Content.Replace("{{ApplicantName}}", result.User.Name);
			document.Content.Replace("{{ApplicantSurname}}", result.User.Surname);
			document.Content.Replace("{{ApplicantAge}}", result.User.Age.ToString());
			document.Content.Replace("{{ApplicantPhoneNumber}}", result.User.ContactPhoneNumber);
			document.Content.Replace("{{ApplicantAddress}}", result.User.Address);
			document.Content.Replace("{{PetName}}", result.Pet.Name);
			document.Content.Replace("{{PetType}}", result.Pet.Type.ToString());
			document.Content.Replace("{{PetGender}}", result.Pet.Gender.ToString());
			document.Content.Replace("{{PetAge}}", result.Pet.Age.ToString());
			document.Content.Replace("{{AdoptionFee}}", result.SumOfAdoptionFee.ToString());
			document.Content.Replace("{{DateOfAdoptionApplication}}", result.ApplicationDate.ToString());
			document.Content.Replace("{{DateSigned}}", DateTime.UtcNow.ToString());

			var stream = new MemoryStream();


			document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }
    }
}
