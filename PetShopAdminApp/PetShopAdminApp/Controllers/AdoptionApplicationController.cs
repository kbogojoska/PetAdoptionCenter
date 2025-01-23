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
	}
}
