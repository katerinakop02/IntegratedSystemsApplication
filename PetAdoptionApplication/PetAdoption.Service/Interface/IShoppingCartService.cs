using PetAdoption.Domain.Domain;
using PetAdoption.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart AddProductToShoppingCart(string userId, AddToCartDTO model);
        AddToCartDTO getProductInfo(Guid Id);
        ShoppingCartDTO getShoppingCartDetails(string userId);
        Boolean deleteFromShoppingCart(string userId, Guid? Id);
        Boolean orderProducts(string userId);
    }
}
