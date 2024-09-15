using PetAdoption.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain.Domain
{
    public class AdoptApplication : BaseEntity
    {
        public string? OwnerId { get; set; }

        public Adopter? Owner { get; set; }

        public ICollection<PetInAdoptApplication>? PetInAdoptApplications { get; set; }  
    }
}
