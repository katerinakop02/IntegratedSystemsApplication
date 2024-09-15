using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain.Domain
{
    public class PetInAdoptApplication : BaseEntity
    {
        public Guid PetId { get; set; }
        public Pet? AdoptedPet { get; set; }
        public Guid ApplicationId { get; set; }
        public AdoptApplication? AdoptApplication { get; set; }
        public int Quantity { get; set; }
    }
}
