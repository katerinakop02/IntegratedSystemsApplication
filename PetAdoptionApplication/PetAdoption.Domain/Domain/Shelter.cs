using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain.Domain
{
    public class Shelter : BaseEntity
    {
        [Required]
        public string ShelterName { get; set; }
        [Required]
        public string ShelterAdress { get; set; }
        [Required]
        public string ShelterImage { get; set; }
        [Required]
        public double Rating { get; set; }

        public virtual ICollection<Pet>? Pets { get; set; }
    }
}
