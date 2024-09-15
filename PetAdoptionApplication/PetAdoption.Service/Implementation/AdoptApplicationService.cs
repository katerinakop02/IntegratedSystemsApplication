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
    public class AdoptApplicationService : IAdoptApplicationService
    {
        private readonly IAdoptApplicationRepository _repository;

        public AdoptApplicationService(IAdoptApplicationRepository repository)
        {
            _repository = repository;
        }

        public List<AdoptApplication> GetAllApplications()
        {
            return _repository.GetAllApplications();    
        }

        public AdoptApplication GetDetailsForApplication(BaseEntity model)
        {
            return _repository.GetDetailsForApplication(model);
        }
    }
}
