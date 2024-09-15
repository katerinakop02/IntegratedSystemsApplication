namespace PetAdoptionAdminApplication.Models
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
