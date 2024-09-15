using PetAdoption.Domain.Domain;
using PetAdoption.Repository.Interface;
using PetAdoption.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Implementation
{
    public class PetService : IPetService
    {
        private readonly IRepository<Pet> _productRepository;
        private readonly IUserRepository _userRepository;

        public PetService(IRepository<Pet> productRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public Pet CreateNewProduct(string userId, Pet product)
        {
            var createdBy = _userRepository.Get(userId);
            product.CreatedBy = createdBy;
            return _productRepository.Insert(product);
        }

        public Pet DeleteProduct(Guid id)
        {
            var product_to_delete = this.GetProductById(id);
            return _productRepository.Delete(product_to_delete);
        }

        public Pet GetProductById(Guid? id)
        {
            return _productRepository.Get(id);
        }

        public List<Pet> GetProducts()
        {
            return _productRepository.GetAll().ToList();
        }

        public Pet UpdateProduct(Pet product)
        {
            return _productRepository.Update(product);
        }
    }
}
