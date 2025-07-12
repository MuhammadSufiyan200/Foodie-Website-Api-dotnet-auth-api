using System.ComponentModel.DataAnnotations;

namespace Authentication_System_API.model
{
    public class ItemCategory
    {
        [Key]
        public int ItemCategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
