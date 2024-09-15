using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAdminApplication.Models
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
