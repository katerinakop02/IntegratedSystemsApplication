using Microsoft.IdentityModel.Tokens;
using PetAdoption.Domain;
using PetAdoption.Domain.Domain;
using PetAdoption.Domain.DTO;
using PetAdoption.Repository.Interface;
using PetAdoption.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Pet> _productRepository;
        private readonly IRepository<AdoptApplication> _applicationRepository;
        private readonly IRepository<PetInAdoptApplication> _productInAdoptApplicationRepository;
        private readonly IRepository<EmailMessage> _mailRepository;
        private readonly IEmailService _emailService;

        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<Pet> productRepository, IRepository<AdoptApplication> applicationRepository, IRepository<PetInAdoptApplication> productInAdoptApplicationRepository, IRepository<EmailMessage> mailRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _applicationRepository = applicationRepository;
            _productInAdoptApplicationRepository = productInAdoptApplicationRepository;
            _mailRepository = mailRepository;
            _emailService = emailService;
        }

        public ShoppingCart AddProductToShoppingCart(string userId, AddToCartDTO model)
        {
            if(userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);
                var userCart = loggedInUser?.UserCart;
                var selectedProduct = _productRepository.Get(model.SelectedProductId);

                if (selectedProduct != null && userCart != null)
                {
                    userCart?.PetInShoppingCarts?.Add(new PetInShoppingCart
                    {
                        Pet = selectedProduct,
                        PetId = selectedProduct.Id,
                        ShoppingCart = userCart,
                        ShoppingCartId = userCart.Id,
                        Quantity = model.Quantity
                    });

                    return _shoppingCartRepository.Update(userCart);
                }
            }
            return null;
        }

        public bool deleteFromShoppingCart(string userId, Guid? Id)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);


                var product_to_delete = loggedInUser?.UserCart?.PetInShoppingCarts.First(z => z.PetId == Id);

                loggedInUser?.UserCart?.PetInShoppingCarts?.Remove(product_to_delete);

                _shoppingCartRepository.Update(loggedInUser.UserCart);

                return true;
            }
            return false;
        }

        public AddToCartDTO getProductInfo(Guid Id)
        {
            var selectedProduct = _productRepository.Get(Id);
            if (selectedProduct != null)
            {
                var model = new AddToCartDTO
                {
                    SelectedProductName = selectedProduct.ShelterId.ToString(),
                    SelectedProductId = selectedProduct.Id,
                    Quantity = 1
                };
                return model;
            }
            return null;
        }

        public ShoppingCartDTO getShoppingCartDetails(string userId)
        {
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = _userRepository.Get(userId);

                var allProducts = loggedInUser?.UserCart?.PetInShoppingCarts?.ToList();

                var totalPrice = 0.0;

                foreach (var item in allProducts)
                {
                    totalPrice += Double.Round((item.Quantity * item.Pet.Price), 2);
                }

                var model = new ShoppingCartDTO
                {
                    AllProducts = allProducts,
                    TotalPrice = totalPrice
                };

                return model;

            }

            return new ShoppingCartDTO
            {
                AllProducts = new List<PetInShoppingCart>(),
                TotalPrice = 0.0
            };
        }

        public bool orderProducts(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                EmailMessage message = new EmailMessage();
                message.MailTo = loggedInUser.Email;
                message.Subject = "Successfully created order";
                message.Status = false;

                AdoptApplication adoptApplication = new AdoptApplication
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    Owner = loggedInUser
                };

                _applicationRepository.Insert(adoptApplication);

                List<PetInAdoptApplication> petInApplication = new List<PetInAdoptApplication>();

                var lista = userShoppingCart.PetInShoppingCarts.Select(
                    x => new PetInAdoptApplication
                    {
                        Id = Guid.NewGuid(),
                        PetId = x.Pet.Id,
                        AdoptedPet = x.Pet,
                        ApplicationId = adoptApplication.Id,
                        AdoptApplication = adoptApplication,
                        Quantity = x.Quantity
                    }
                    ).ToList();

                StringBuilder sb = new StringBuilder();
                var totalPrice = 0.0;
                sb.AppendLine("Your order is completed. The order contains: ");

                for (int i=1; i<=lista.Count; i++)
                {
                    var currentItem = lista[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.AdoptedPet.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.AdoptedPet.Id + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.AdoptedPet.Price);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());
                message.Content = sb.ToString();

                petInApplication.AddRange(lista);

                foreach (var product in petInApplication)
                {
                    _productInAdoptApplicationRepository.Insert(product);
                }

                loggedInUser.UserCart.PetInShoppingCarts.Clear();
                _userRepository.Update(loggedInUser);
                _emailService.SendEmailAsync(message);

                return true;
            }
            return false;
        }
    }
}
