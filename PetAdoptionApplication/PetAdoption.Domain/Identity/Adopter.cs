using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PetAdoption.Domain.Domain;

namespace PetAdoption.Domain.Identity
{
    public class Adopter : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public string? Address { get; set; }
        public ShoppingCart? UserCart { get; set; }
        public virtual ICollection<AdoptApplication>? Applications { get; set; }
    }
}
