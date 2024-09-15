using PetAdoption.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }

        public Adopter? Owner { get; set; }
        
        public virtual ICollection<PetInShoppingCart>? PetInShoppingCarts { get; set; }
    }
}
