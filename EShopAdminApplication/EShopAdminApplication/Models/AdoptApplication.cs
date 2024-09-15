namespace PetAdoptionAdminApplication.Models
{
    public class AdoptApplication : BaseEntity
    {
        public string? OwnerId { get; set; }

        public Adopter? Owner { get; set; }

        public ICollection<PetInAdoptApplication>? PetInAdoptApplications { get; set; }
    }
}
