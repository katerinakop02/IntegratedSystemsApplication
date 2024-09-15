using PetAdoption.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<Adopter> GetAll();
        Adopter Get(string id);
        void Insert(Adopter entity);
        void Update(Adopter entity);
        void Delete(Adopter entity);
    }
}
