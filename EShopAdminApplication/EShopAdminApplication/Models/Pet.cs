namespace PetAdoptionAdminApplication.Models
{
    public class Pet : BaseEntity
    {
        public Guid ShelterId { get; set; }
        public Shelter? Shelter { get; set; }
        public double Price { get; set; }

        public virtual Adopter? CreatedBy { get; set; }
        public virtual ICollection<PetInShoppingCart>? PetsInShoppingCart { get; set; }
        public ICollection<PetInAdoptApplication>? PetInAdoptApplications { get; set; }


    }
}
