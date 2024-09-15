using PetAdoption.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<PetInShoppingCart>? AllProducts { get; set; }
        public double TotalPrice { get; set; }
    }
}
