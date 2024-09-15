namespace PetAdoptionAdminApplication.Models
{
    public class PetInShoppingCart : BaseEntity
    {
        public Guid PetId { get; set; }
        public Pet? Pet { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
