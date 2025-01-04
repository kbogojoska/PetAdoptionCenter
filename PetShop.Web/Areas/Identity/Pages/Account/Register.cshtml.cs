using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PetShop.Domain.Identity; // Import your custom user class

namespace PetShop.Web.Areas.Identity.Pages.Account
{
	public class RegisterModel : PageModel
	{
		private readonly SignInManager<PetShopApplicationUser> _signInManager;
		private readonly UserManager<PetShopApplicationUser> _userManager;
		private readonly IUserStore<PetShopApplicationUser> _userStore;
		private readonly IUserEmailStore<PetShopApplicationUser> _emailStore;
		private readonly ILogger<RegisterModel> _logger;
		private readonly IEmailSender _emailSender;

		public RegisterModel(
			UserManager<PetShopApplicationUser> userManager,
			IUserStore<PetShopApplicationUser> userStore,
			SignInManager<PetShopApplicationUser> signInManager,
			ILogger<RegisterModel> logger,
			IEmailSender emailSender)
		{
			_userManager = userManager;
			_userStore = userStore;
			_emailStore = GetEmailStore();
			_signInManager = signInManager;
			_logger = logger;
			_emailSender = emailSender;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public string ReturnUrl { get; set; }

		public IList<AuthenticationScheme> ExternalLogins { get; set; }

		public class InputModel
		{
			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; }

			[Required]
			[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }

			// Custom fields
			[Required]
			[Display(Name = "First Name")]
			public string Name { get; set; }

			[Required]
			[Display(Name = "Last Name")]
			public string Surname { get; set; }

			[Required]
			[Range(1, 120, ErrorMessage = "Please enter a valid age.")]
			public int Age { get; set; }

			[Required]
			[Phone]
			[Display(Name = "Contact Phone Number")]
			public string ContactPhoneNumber { get; set; }

			[Required]
			[Display(Name = "City of Residence")]
			public string CityOfResidence { get; set; }

			[Required]
			[Display(Name = "Address")]
			public string Address { get; set; }
		}

		public async Task OnGetAsync(string returnUrl = null)
		{
			ReturnUrl = returnUrl;
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/");
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

			if (ModelState.IsValid)
			{
				var user = CreateUser();

				// Set custom properties
				user.Name = Input.Name;
				user.Surname = Input.Surname;
				user.Age = Input.Age;
				user.ContactPhoneNumber = Input.ContactPhoneNumber;
				user.CityOfResidence = Input.CityOfResidence;
				user.Address = Input.Address;

				await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
				await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
				var result = await _userManager.CreateAsync(user, Input.Password);

				if (result.Succeeded)
				{
					_logger.LogInformation("User created a new account with password.");

					var userId = await _userManager.GetUserIdAsync(user);
					var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
					var callbackUrl = Url.Page(
						"/Account/ConfirmEmail",
						pageHandler: null,
						values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
						protocol: Request.Scheme);

					await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
						$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

					if (_userManager.Options.SignIn.RequireConfirmedAccount)
					{
						return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
					}
					else
					{
						await _signInManager.SignInAsync(user, isPersistent: false);
						return LocalRedirect(returnUrl);
					}
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return Page();
		}

		private PetShopApplicationUser CreateUser()
		{
			try
			{
				return Activator.CreateInstance<PetShopApplicationUser>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(PetShopApplicationUser)}'. " +
					$"Ensure that '{nameof(PetShopApplicationUser)}' is not an abstract class and has a parameterless constructor.");
			}
		}

		private IUserEmailStore<PetShopApplicationUser> GetEmailStore()
		{
			if (!_userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("The default UI requires a user store with email support.");
			}
			return (IUserEmailStore<PetShopApplicationUser>)_userStore;
		}
	}
}
