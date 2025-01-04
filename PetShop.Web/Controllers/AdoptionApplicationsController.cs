using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace PetShop.Web.Controllers
{
    public class AdoptionApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        protected readonly IPetService _petService;
        protected readonly IShelterService _shelterService;
        protected readonly UserManager<PetShopApplicationUser> _userManager;
        protected readonly IAdoptionApplicationService _adoptionApplicationService;

		public AdoptionApplicationsController(ApplicationDbContext context, IPetService petService, UserManager<PetShopApplicationUser> userManager, IShelterService shelterService, IAdoptionApplicationService adoptionApplicationService)
		{
			_context = context;
			this._petService = petService;
			_userManager = userManager;
			_shelterService = shelterService;
			_adoptionApplicationService = adoptionApplicationService;
		}

		// GET: AdoptionApplications
		public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdoptionApplications.Include(a => a.Pet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdoptionApplications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionApplication = await _context.AdoptionApplications
                .Include(a => a.Pet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionApplication == null)
            {
                return NotFound();
            }

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
            Console.WriteLine(pet.ShelterOfResidenceId);
            var shelterDto = _shelterService.FindById(pet.ShelterOfResidenceId.ToString());

            var currentUser = _userManager.GetUserAsync(User).Result;
            if (currentUser == null)
            {
                return Unauthorized("You must be logged in to adopt a pet.");
            }

            var adoptionApplication = new AdoptionApplication
            {
                Pet = pet.ToPet(shelterDto.toShelter()),
                PetId = petId,
                Applicant = currentUser,
                ApplicationDate = DateTime.UtcNow,
                SumOfAdoptionFee = pet.PriceForAdoption,
                IsValid = pet.isAvailable && currentUser.Age >= 18
            };

            //ViewData["PetId"] = new SelectList(_context.Pets, "Id", "ImageURL");
            return View(adoptionApplication);
        }

        // POST: AdoptionApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdoptionApplicationDTO adoptionApplicationDTO)
        {
			if (ModelState.IsValid)
			{
				try
				{
					var result = _adoptionApplicationService.Store(adoptionApplicationDTO);
					return RedirectToAction("Index", "AdoptionApplications");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message); // Display the error to the user
				}
			}


			ViewData["PetId"] = new SelectList(_context.Pets, "Id", "ImageURL", adoptionApplicationDTO.PetId);
            return View(adoptionApplicationDTO);
        }

        // GET: AdoptionApplications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionApplication = await _context.AdoptionApplications.FindAsync(id);
            if (adoptionApplication == null)
            {
                return NotFound();
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "ImageURL", adoptionApplication.PetId);
            return View(adoptionApplication);
        }

        // POST: AdoptionApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PetId,IsValid,ApplicationDate,SumOfAdoptionFee,Id")] AdoptionApplication adoptionApplication)
        {
            if (id != adoptionApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoptionApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionApplicationExists(adoptionApplication.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "ImageURL", adoptionApplication.PetId);
            return View(adoptionApplication);
        }

        // GET: AdoptionApplications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionApplication = await _context.AdoptionApplications
                .Include(a => a.Pet)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var adoptionApplication = await _context.AdoptionApplications.FindAsync(id);
            if (adoptionApplication != null)
            {
                _context.AdoptionApplications.Remove(adoptionApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionApplicationExists(Guid id)
        {
            return _context.AdoptionApplications.Any(e => e.Id == id);
        }
    }
}
