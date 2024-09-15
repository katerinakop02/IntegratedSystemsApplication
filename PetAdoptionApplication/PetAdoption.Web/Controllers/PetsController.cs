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
    public class PetsController : Controller
    {
        private readonly IPetService _petService;
        private readonly IShelterService _shelterService;
        private readonly IShoppingCartService _shoppingCartService;

        public PetsController(IPetService petService, IShelterService shelterService, IShoppingCartService shoppingCartService)
        {
            _petService = petService;
            _shelterService = shelterService;
            _shoppingCartService = shoppingCartService;
        }

        // GET: Pet
        public IActionResult Index()
        {
            return View(_petService.GetProducts());
        }

        // GET: Pet/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetProductById(id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pet/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.ShelterId = new SelectList(_shelterService.GetAllShelters(), "Id", "ShelterName");
            return View();
        }

        // POST: Pet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Id,ShelterId,Name,Breed,Age,Gender,Price,PetImage")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
                pet.Id = Guid.NewGuid(); // Ensure ID is generated for new pet
                _petService.CreateNewProduct(loggedInUser, pet);
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // GET: Pet/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetProductById(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        // POST: Pet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Guid id, [Bind("Id,ShelterId,Name,Breed,Age,Gender,Price,PetImage")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _petService.UpdateProduct(pet);
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // GET: Pet/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetProductById(id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _petService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]

        public IActionResult AddPetToCart(Guid Id)
        {
            var result = _shoppingCartService.getProductInfo(Id);
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddPetToCart(AddToCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _shoppingCartService.AddProductToShoppingCart(userId, model);

            if (result != null)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else { return View(model); }
        }

        private bool ProductExists(Guid id)
        {
            return _petService.GetProductById(id) != null;
        }
    }
}
