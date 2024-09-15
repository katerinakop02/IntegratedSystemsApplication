using PetAdoption.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Repository.Interface
{
    public interface IAdoptApplicationRepository
    {
        List<AdoptApplication> GetAllApplications();
        AdoptApplication GetDetailsForApplication(BaseEntity model);
    }
}
