using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain.Domain;
using PetAdoption.Domain.DTO;
using PetAdoption.Repository;
using PetAdoption.Service.Implementation;
using PetAdoption.Service.Interface;

namespace PetAdoption.Web.Controllers
{
    public class SheltersController : Controller
    {
        private readonly IShelterService _shelterService;

        public SheltersController(IShelterService shelterService, IShoppingCartService shoppingCartService, IPetService productService)
        {
            _shelterService = shelterService;
        }

        // GET: Shelter
        public IActionResult Index()
        {
            var shelters = _shelterService.GetAllShelters();
            return View(shelters);
        }

        // GET: Shelter/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = _shelterService.GetDetailsForShelter(id);
            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // GET: Shelter/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shelter/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ShelterName,ShelterAdress,ShelterImage,Rating")] Shelter shelter)
        {
            if (ModelState.IsValid)
            {
                shelter.Id = Guid.NewGuid();
                _shelterService.CreateNewShelter(shelter);
                return RedirectToAction(nameof(Index));
            }
            return View(shelter);
        }

        // GET: Shelter/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = _shelterService.GetDetailsForShelter(id);
            if (shelter == null)
            {
                return NotFound();
            }
            return View(shelter);
        }

        // POST: Shelter/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,ShelterName,ShelterAdress,ShelterImage,Rating")] Shelter shelter)
        {
            if (id != shelter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _shelterService.UpdateExistingShelter(shelter);
                }
                catch (Exception)
                {
                    // Handle exceptions (e.g., when entity is not found or concurrency issues)
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shelter);
        }

        // GET: Shelter/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = _shelterService.GetDetailsForShelter(id);
            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // POST: Shelter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _shelterService.DeleteShelter(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
