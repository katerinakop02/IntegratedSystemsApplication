using PetAdoption.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Interface
{
    public interface IAdoptApplicationService
    {
        List<AdoptApplication> GetAllApplications();
        AdoptApplication GetDetailsForApplication(BaseEntity model);
    }
}
