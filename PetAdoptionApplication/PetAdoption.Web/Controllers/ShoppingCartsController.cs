using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain.Domain;
using PetAdoption.Repository;
using PetAdoption.Service.Interface;
using Stripe;

namespace PetAdoption.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCart
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(_shoppingCartService.getShoppingCartDetails(userId ?? ""));
        }

        // POST: ShoppingCart/Delete/5
        public async Task<IActionResult> DeletePetFromShoppingCart(Guid? petId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            var result = _shoppingCartService.deleteFromShoppingCart(userId, petId);

            return RedirectToAction("Index", "ShoppingCarts");
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.getShoppingCartDetails(userId ?? "");

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice)*100),
                Description = "PetAdoption Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if(charge.Status == "succeeded")
            {
                var result = this.Order();

                if(result)
                {
                    return RedirectToAction("Index", "ShoppingCarts");
                }
                else
                {
                    return RedirectToAction("Index", "ShoppingCarts");
                }
            }

            return RedirectToAction("Index", "ShoppingCarts");
        }

        // POST: ShoppingCart/Order
        private Boolean Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            var result = _shoppingCartService.orderProducts(userId ?? "");

            //return RedirectToAction("Index", "ShoppingCarts");

            return result;
        }
    }
}
