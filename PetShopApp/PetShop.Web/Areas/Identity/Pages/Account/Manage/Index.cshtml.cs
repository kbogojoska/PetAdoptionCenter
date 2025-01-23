using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using PetShop.Domain.Identity;
using System.ComponentModel.DataAnnotations;

public class IndexModel : PageModel
{
	private readonly UserManager<PetShopApplicationUser> _userManager;
	private readonly SignInManager<PetShopApplicationUser> _signInManager;

	public IndexModel(
		UserManager<PetShopApplicationUser> userManager,
		SignInManager<PetShopApplicationUser> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	public string Username { get; set; }

	[TempData]
	public string StatusMessage { get; set; }

	[BindProperty]
	public InputModel Input { get; set; }

	public class InputModel
	{
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Display(Name = "First Name")]
		public string Name { get; set; }

		[Display(Name = "Last Name")]
		public string Surname { get; set; }

		[Range(1, 120, ErrorMessage = "Please enter a valid age.")]
		[Display(Name = "Age")]
		public int Age { get; set; }

		[Phone]
		[Display(Name = "Contact Phone Number")]
		public string ContactPhoneNumber { get; set; }

		[Display(Name = "City of Residence")]
		public string CityOfResidence { get; set; }

		[Display(Name = "Address")]
		public string Address { get; set; }
	}

	private async Task LoadAsync(PetShopApplicationUser user)
	{
		var userName = await _userManager.GetUserNameAsync(user);

		Username = userName;

		Input = new InputModel
		{
			Email = user.Email,
			Name = user.Name,
			Surname = user.Surname,
			Age = user.Age,
			ContactPhoneNumber = user.ContactPhoneNumber,
			CityOfResidence = user.CityOfResidence,
			Address = user.Address
		};
	}

	public async Task<IActionResult> OnGetAsync()
	{
		var user = await _userManager.GetUserAsync(User);
		if (user == null)
		{
			return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
		}

		await LoadAsync(user);
		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		var user = await _userManager.GetUserAsync(User);
		if (user == null)
		{
			return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
		}

		if (!ModelState.IsValid)
		{
			await LoadAsync(user);
			return Page();
		}

		user.Email = Input.Email;
		user.Name = Input.Name;
		user.Surname = Input.Surname;
		user.Age = Input.Age;
		user.ContactPhoneNumber = Input.ContactPhoneNumber;
		user.CityOfResidence = Input.CityOfResidence;
		user.Address = Input.Address;

		var updateResult = await _userManager.UpdateAsync(user);
		if (!updateResult.Succeeded)
		{
			StatusMessage = "Unexpected error when trying to update user information.";
			return RedirectToPage();
		}

		await _signInManager.RefreshSignInAsync(user);
		StatusMessage = "Your profile has been updated";
		return RedirectToPage();
	}
}
