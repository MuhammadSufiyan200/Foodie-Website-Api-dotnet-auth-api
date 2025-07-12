using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication_System_API.model
{
    public class InventoryItem
    {
        [Key]
        public int InventoryItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        // Foreign Keys
        public int ItemCategoryId { get; set; }
        [ForeignKey("ItemCategoryId")]
        public ItemCategory ItemCategory { get; set; }

        public int UnitId { get; set; }
        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }
        public string ImagePath { get; set; }
    }
}
