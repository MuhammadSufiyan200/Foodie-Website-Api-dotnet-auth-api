using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Authentication_System_API.model
{
    public class SaleDetail
    {
        [Key]
        public int SaleDetailId { get; set; }

        public int SaleRootId { get; set; }
        [ForeignKey("SaleRootId")]
        public SaleRoot SaleRoot { get; set; }

        public int InventoryItemId { get; set; }
        [ForeignKey("InventoryItemId")]
        public InventoryItem InventoryItem { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
