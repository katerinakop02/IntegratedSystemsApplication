using PetAdoption.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Domain.Domain
{
    public class Pet : BaseEntity
    {
        public Guid ShelterId { get; set; }
        public virtual Shelter? Shelter { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Breed { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public string Gender { get; set; }
        public double Price { get; set; }
        [Required]
        public string PetImage {  get; set; }

        public virtual Adopter? CreatedBy { get; set; }
        public virtual ICollection<PetInShoppingCart>? PetsInShoppingCart { get; set; }
        public ICollection<PetInAdoptApplication>? PetInAdoptApplications { get; set; } 

        
    }
}
