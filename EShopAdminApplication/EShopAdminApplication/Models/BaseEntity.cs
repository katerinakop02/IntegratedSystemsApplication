using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAdminApplication.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
