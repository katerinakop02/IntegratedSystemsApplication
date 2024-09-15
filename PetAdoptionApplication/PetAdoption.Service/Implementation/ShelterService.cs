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
    public class ShelterService : IShelterService
    {
        private readonly IRepository<Shelter> _shelterRepository;

        public ShelterService(IRepository<Shelter> shelterRepository)
        {
            _shelterRepository = shelterRepository;
        }

        public void CreateNewShelter(Shelter p)
        {
            _shelterRepository.Insert(p);
        }

        public void DeleteShelter(Guid id)
        {
            Shelter shelter = _shelterRepository.Get(id);
            _shelterRepository.Delete(shelter);
        }

        public List<Shelter> GetAllShelters()
        {
            return _shelterRepository.GetAll().ToList();
        }

        public Shelter GetDetailsForShelter(Guid? id)
        {
            return _shelterRepository.Get(id);
        }

        public void UpdateExistingShelter(Shelter p)
        {
            _shelterRepository.Update(p);
        }
    }
}
