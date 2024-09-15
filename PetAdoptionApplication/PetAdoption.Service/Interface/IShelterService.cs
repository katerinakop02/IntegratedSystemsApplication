using PetAdoption.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Interface
{
    public interface IShelterService
    {
        List<Shelter> GetAllShelters();
        Shelter GetDetailsForShelter(Guid? id);
        void CreateNewShelter(Shelter p);
        void UpdateExistingShelter(Shelter p);
        void DeleteShelter(Guid id);
    }
}
