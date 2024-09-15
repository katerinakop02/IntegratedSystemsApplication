namespace PetAdoptionAdminApplication.Models
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }

        public Adopter? Owner { get; set; }

        public virtual ICollection<PetInShoppingCart>? PetInShoppingCarts { get; set; }
    }
}
