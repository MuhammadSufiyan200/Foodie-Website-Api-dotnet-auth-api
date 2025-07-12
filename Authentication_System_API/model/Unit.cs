using System.ComponentModel.DataAnnotations;

namespace Authentication_System_API.model
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }

        [Required]
        public string UnitName { get; set; }

        public ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
