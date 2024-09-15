using PetAdoption.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Interface
{
    public interface IPetService
    {
        public List<Pet> GetProducts();
        public Pet GetProductById(Guid? id);
        public Pet CreateNewProduct(string userId, Pet product);
        public Pet UpdateProduct(Pet product);
        public Pet DeleteProduct(Guid id);
    }
}
