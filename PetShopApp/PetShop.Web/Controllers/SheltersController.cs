using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using PetShop.Repository;
using PetShop.Service.Implementation;
using PetShop.Service.Interface;

namespace PetShop.Web.Controllers
{
    [Authorize]
    public class SheltersController : Controller
    {
        private readonly ApplicationDbContext _context;
        protected readonly IShelterService _shelterService;
        protected readonly IPetService _petService;

        public SheltersController(ApplicationDbContext context, IShelterService shelterService, IPetService petService)
        {
            _context = context;
            _shelterService = shelterService;
            _petService = petService;
        }

        // GET: Shelters
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(_shelterService.FindAll());
        }

        // GET: Shelters/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = _shelterService.FindById(id.ToString());
            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // GET: Shelters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shelters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShelterDTO shelterDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var shelter = _shelterService.Store(shelterDTO);

                    TempData["Success"] = "You have successfully adopted the pet!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Something went wrong.";
                }
            }
            return View(shelterDTO);
        }

        // GET: Shelters/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = _shelterService.FindById(id.ToString());
            if (shelter == null)
            {
                return NotFound();
            }
            return View(shelter);
        }

        // POST: Shelters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ShelterDTO shelterDTO)
        {
            if (id != shelterDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _shelterService.Update(id.ToString(), shelterDTO);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Something went wrong.";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shelterDTO);
        }

        // GET: Shelters/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = _shelterService.FindById(id.ToString());
            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // POST: Shelters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shelter = _shelterService.FindById(id.ToString());
            if (shelter != null)
            {
                /*foreach(var pet in shelter.Pets)
                {
                    _petService.DeleteById(pet.Id);
                }*/
                _shelterService.DeleteById(id.ToString());
            }
            else
            {
                return NotFound();

            }

            return RedirectToAction(nameof(Index));
        }

        /*private bool ShelterExists(Guid id)
        {
            return _context.Shelters.Any(e => e.Id == id);
        }*/
    }
}
