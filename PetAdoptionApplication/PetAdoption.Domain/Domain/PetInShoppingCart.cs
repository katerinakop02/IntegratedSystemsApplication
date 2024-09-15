using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain.Domain
{
    public class PetInShoppingCart : BaseEntity
    {
        public Guid PetId { get; set; }
        public Pet? Pet { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
