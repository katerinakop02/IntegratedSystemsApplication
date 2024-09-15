using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain;
using PetAdoption.Domain.Domain;
using PetAdoption.Domain.Identity;

namespace PetAdoption.Repository
{
    public class ApplicationDbContext : IdentityDbContext<Adopter>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Shelter> Shelters { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<AdoptApplication> AdoptApplications { get; set; }
        public virtual DbSet<PetInShoppingCart> PetsInShoppingCarts { get; set; }
        public virtual DbSet<PetInAdoptApplication> PetInAdoptApplications { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
    }
}
