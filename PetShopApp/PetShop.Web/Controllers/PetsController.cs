﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using PetShop.Domain.Enum;
using PetShop.Repository;
using PetShop.Service.Implementation;
using PetShop.Service.Interface;
using PetShop.Service.Mappers;

namespace PetShop.Web.Controllers
{
    [Authorize]
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IShelterService _shelterService;
        private readonly IPetService _petService;

        public PetsController(ApplicationDbContext context, IShelterService shelterService, IPetService petService)
        {
            _context = context;
            _shelterService = shelterService;
            _petService = petService;
        }

        // GET: Pets
        [AllowAnonymous]
        public IActionResult Index(Guid? shelterId, string? city=null, bool? isAvailable = null)
        {
            var shelters = _shelterService.FindAll();

            var cities = shelters.Select(s => s.City).Distinct().ToList();
            ViewData["Shelters"] = shelters; 
            ViewData["Cities"] = cities;
            ViewData["isAvailable"] = isAvailable ?? false;

			List<RequestPetDTO> pets;

            if (shelterId.HasValue && city!=null)
            {
                pets = _petService.FindByShelter(shelterId.Value, isAvailable); 
                ViewData["SelectedShelterId"] = shelterId.Value;  
            }
            else if(shelterId.HasValue)
            {
                pets = _petService.FindByShelter(shelterId.Value, isAvailable); 
                ViewData["SelectedShelterId"] = shelterId.Value;
            }
            else if(city!=null)
            {
                pets = _petService.FindByCity(city, isAvailable); 
				ViewData["SelectedCity"] = city;
			}
            else
            {
                pets = _petService.FindAll(isAvailable); 
            }

            return View(pets);
        }



        // GET: Pets/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var pet = _petService.FindById(id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            
            var shelters = _shelterService.FindAll();
            var sizes = Enum.GetValues(typeof(SizeOfAnimal)).Cast<SizeOfAnimal>().ToList();
            var animalTypes = Enum.GetValues(typeof(AnimalType)).Cast<AnimalType>().ToList();
            var genders = Enum.GetValues(typeof(GenderType)).Cast<GenderType>().ToList();

            ViewData["Shelters"] = shelters;
            ViewData["Sizes"] = sizes;
            ViewData["AnimalTypes"] = animalTypes;
            ViewData["Genders"] = genders;

            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestPetDTO requestPetDto)
        {
            var shelters = _shelterService.FindAll();
            var sizes = Enum.GetValues(typeof(SizeOfAnimal)).Cast<SizeOfAnimal>().ToList();
            var animalTypes = Enum.GetValues(typeof(AnimalType)).Cast<AnimalType>().ToList();
            var genders = Enum.GetValues(typeof(GenderType)).Cast<GenderType>().ToList();

            ViewData["Shelters"] = shelters;
            ViewData["Sizes"] = sizes;
            ViewData["AnimalTypes"] = animalTypes;
            ViewData["Genders"] = genders;

            if (!ModelState.IsValid)
            {
                return View(requestPetDto);
            }

            try
            {
                var createdPet = _petService.Store(requestPetDto);
                //var shelter = _shelterService.FindById(createdPet.ShelterOfResidenceId.ToString());
                //shelter.Pets.Add(createdPet.ToPet(shelter.toShelter()));
                //_shelterService.Update(shelter.Id.ToString(), shelter);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(requestPetDto);
            }
        }




        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound(); 
            }

            var pet = _petService.FindById(id.Value);
            if (pet == null)
            {
                return NotFound();
            }
            if (!pet.isAvailable)
            {
                return View("CannotEdit", pet);
            }
            ViewData["Genders"] = Enum.GetValues(typeof(GenderType)).Cast<GenderType>().ToList();
            ViewData["Types"] = Enum.GetValues(typeof(AnimalType)).Cast<AnimalType>().ToList();
            ViewData["Sizes"] = Enum.GetValues(typeof(SizeOfAnimal)).Cast<SizeOfAnimal>().ToList();
            ViewData["Shelters"] = _shelterService.FindAll();
            return View(pet);
         }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RequestPetDTO requestPetDto)
        {
            if (id != requestPetDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(requestPetDto);
            }

            try
            {
                var shelter = _shelterService.FindById(requestPetDto.ShelterOfResidenceId.ToString());
                if (shelter == null)
                {
                    throw new Exception("Shelter not found.");
                }
                _petService.Update(id, requestPetDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(requestPetDto);
            }
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var pet = _petService.FindById(id.Value);
            if (pet == null)
            {
                return NotFound();
            }

            if(!pet.isAvailable)
            {
				return View("CannotDelete", pet);
			}

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pet = _petService.DeleteById(id); 

            if (pet != null)
            {
                return RedirectToAction(nameof(Index)); 
            }

            return NotFound();
        }

        /*private bool PetExists(Guid id)
        {
            return _context.Pets.Any(e => e.Id == id);
        }*/

        public async Task<IActionResult> Adopt(Guid id)
        {
            var pet = _petService.FindById(id);
            if (pet != null)
            {
                return RedirectToAction(nameof(AdoptionApplicationsController.Create), "AdoptionApplications", new { petId = id });
            }

            return NotFound();
        }
    }
}
