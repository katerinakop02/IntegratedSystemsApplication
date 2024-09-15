using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain.Identity;
using PetAdoption.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Adopter> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Adopter>();
        }

        public void Delete(Adopter entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
               
        }

        public Adopter Get(string id)
        {
            var strGuid = id.ToString();
            return entities
                .Include(z => z.UserCart)
                .Include(z => z.UserCart.PetInShoppingCarts)
                .Include("UserCart.PetInShoppingCarts.Pet")
                .First(s => s.Id == strGuid);
        }

        public IEnumerable<Adopter> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(Adopter entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Adopter entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
