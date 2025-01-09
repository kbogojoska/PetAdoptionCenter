using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using PetShop.Domain.Identity;
using PetShop.Repository;
using PetShop.Service.Interface;
using PetShop.Service.Mappers;
using static System.Net.Mime.MediaTypeNames;

namespace PetShop.Web.Controllers
{
    public class AdoptionApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        protected readonly IPetService _petService;
        protected readonly IShelterService _shelterService;
        protected readonly IUserService _userService;
        protected readonly UserManager<PetShopApplicationUser> _userManager;
        protected readonly IAdoptionApplicationService _adoptionApplicationService;

        public AdoptionApplicationsController(ApplicationDbContext context, IPetService petService, UserManager<PetShopApplicationUser> userManager, IShelterService shelterService, IAdoptionApplicationService adoptionApplicationService, IUserService userService)
        {
            _context = context;
            this._petService = petService;
            _userManager = userManager;
            _shelterService = shelterService;
            _adoptionApplicationService = adoptionApplicationService;
            _userService = userService;
        }

        // GET: AdoptionApplications
        public async Task<IActionResult> Index()
        {
            var applications = _adoptionApplicationService.FindAll();
            ViewData["Users"] = applications.Select(a =>
            {
                var user = _userService.FindById(a.ApplicantId);
                return user;
            }).ToList();
            ViewData["Pets"] = applications.Select(a =>
            {
                var pet = _petService.FindById(a.PetId);
                return pet.toResponsePetDto();
            }).ToList();
            return View(applications);
        }

        // GET: AdoptionApplications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionApplication = _adoptionApplicationService.FindById(id.ToString());
            if (adoptionApplication == null)
            {
                return NotFound();
            }
            ViewData["User"] = _userService.FindById(adoptionApplication.ApplicantId);
            ViewData["Pet"] = _petService.FindById(adoptionApplication.PetId);

            return View(adoptionApplication);
        }

        // GET: AdoptionApplications/Create
        [HttpGet]
        public IActionResult Create(Guid petId)
        {
            var pet = _petService.FindById(petId);
            if (pet == null || !pet.isAvailable)
            {
                return NotFound("Pet not found or not available for adoption.");
            }
            var shelterDto = _shelterService.FindById(pet.ShelterOfResidenceId.ToString());

            var currentUser = _userManager.GetUserAsync(User).Result;
            if (currentUser == null)
            {
                return Unauthorized("You must be logged in to adopt a pet.");
            }

            var adoptionApplication = new AdoptionApplication
            {
                ApplicantId = currentUser.Id,
                Pet = pet.ToPet(shelterDto.toShelter()),
                PetId = petId,
                Applicant = currentUser,
                ApplicationDate = DateTime.UtcNow,
                SumOfAdoptionFee = pet.PriceForAdoption,
                IsValid = pet.isAvailable && currentUser.Age >= 18
            };

            var adoptionAppForView = adoptionApplication.toDTO();

            ViewData["Pet"] = pet.ToPet(shelterDto.toShelter());
            ViewData["Applicant"] = currentUser;

            return View(adoptionAppForView);
        }

        // POST: AdoptionApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdoptionApplicationDTO adoptionApplicationDTO)
        {

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized("You must be logged in to adopt a pet.");
            }

            var petAdopted = _petService.FindById(adoptionApplicationDTO.PetId);

            if (currentUser.Age >= 18 && petAdopted != null && petAdopted.isAvailable)
            {
                adoptionApplicationDTO.IsValid = true;
            }
            
            if (ModelState.IsValid && adoptionApplicationDTO.IsValid)
            {
                try
                {
                    petAdopted.isAvailable = false;
                    _petService.Update(petAdopted.Id, petAdopted);

                    var result = _adoptionApplicationService.Store(adoptionApplicationDTO);                  

                    TempData["Success"] = "You have successfully adopted the pet!";
                    return RedirectToAction("Index", "Pets");
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Something went wrong.";
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Something went wrong.";
                if (currentUser.Age < 18)
                {
                    ViewData["ErrorMessage"] = "User must be above the age of 18 to adopt an animal";
                }
            }

            var pet = _petService.FindById(adoptionApplicationDTO.PetId);
            if (pet == null)
            {
                return NotFound("Pet not found.");
            }

            ViewData["Pet"] = pet;
            ViewData["Applicant"] = currentUser;

            return View(adoptionApplicationDTO);
        }

        // GET: AdoptionApplications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionApplication = _adoptionApplicationService.FindById(id.ToString());
            if (adoptionApplication == null)
            {
                return NotFound();
            }

            ViewData["Pet"] = _petService.FindById(adoptionApplication.PetId);
            ViewData["User"] = _userService.FindById(adoptionApplication.ApplicantId);
            ViewData["Pets"] = _petService.FindAll().Where(p => p.isAvailable == true).ToList();

            return View(adoptionApplication);
        }

        // POST: AdoptionApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AdoptionApplicationDTO adoptionApplicationDTO)
        {
            if (id != adoptionApplicationDTO.Id)
            {
                return NotFound();
            }
            var prevApp = _adoptionApplicationService.FindById(adoptionApplicationDTO.Id.ToString());

            if (ModelState.IsValid)
            {
                try
                {
                    var petPrevAdopted = _petService.FindById(prevApp.PetId);

                    if (prevApp != null && prevApp.PetId != adoptionApplicationDTO.PetId)
                    {
                        petPrevAdopted.isAvailable = true;
                        _petService.Update(petPrevAdopted.Id, petPrevAdopted);

                        var newPet = _petService.FindById(adoptionApplicationDTO.PetId);
                        newPet.isAvailable = false;
                        _petService.Update(newPet.Id, newPet);
                    }

                    _adoptionApplicationService.Update(adoptionApplicationDTO.Id.ToString(), adoptionApplicationDTO);
                }
                catch (Exception ex)
                {
                    var petPrevAdopted = _petService.FindById(prevApp.PetId);

                    if (prevApp != null && prevApp.PetId != adoptionApplicationDTO.PetId)
                    {
                        petPrevAdopted.isAvailable = false;
                        _petService.Update(petPrevAdopted.Id, petPrevAdopted);

                        var newPet = _petService.FindById(adoptionApplicationDTO.PetId);
                        newPet.isAvailable = true;
                        _petService.Update(newPet.Id, newPet);
                    }

                    ViewData["ErrorMessage"] = "Something went wrong.";
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pet"] = _petService.FindById(adoptionApplicationDTO.PetId);
            ViewData["User"] = _userService.FindById(adoptionApplicationDTO.ApplicantId);
            ViewData["Pets"] = _petService.FindAll().Where(p => p.isAvailable == true).ToList();

            return View(adoptionApplicationDTO);
        }

        // GET: AdoptionApplications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionApplication = _adoptionApplicationService.FindById(id.ToString());
            ViewData["Pet"] = _petService.FindById(adoptionApplication.PetId).toResponsePetDto();
            ViewData["User"] = _userService.FindById(adoptionApplication.ApplicantId);
            if (adoptionApplication == null)
            {
                return NotFound();
            }

            return View(adoptionApplication);
        }

        // POST: AdoptionApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adoptionApplication = _adoptionApplicationService.FindById(id.ToString());
            if (adoptionApplication != null)
            {
                var petAdoptedDto = _petService.FindById(adoptionApplication.PetId);
                petAdoptedDto.isAvailable = true;
                _petService.Update(petAdoptedDto.Id, petAdoptedDto);

                var shelter = _shelterService.FindById(petAdoptedDto.ShelterOfResidenceId.ToString()).toShelter();

                var pet = petAdoptedDto.ToPet(shelter);

                var user = _userService.FindById(adoptionApplication.ApplicantId);
                if (user == null)
                    throw new InvalidOperationException($"User with ID {adoptionApplication.ApplicantId} not found");

                var countBefore = user.AdoptionApplications;

                user.AdoptionApplications.Remove(adoptionApplication.toAdopApp(user.toApplicationUser(), pet));

                _userService.Update(adoptionApplication.ApplicantId, user);

                var countAfter = user.AdoptionApplications;

                _adoptionApplicationService.DeleteById(id.ToString());
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionApplicationExists(Guid id)
        {
            return _context.AdoptionApplications.Any(e => e.Id == id);
        }
    }
}
