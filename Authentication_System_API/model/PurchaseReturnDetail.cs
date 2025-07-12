using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Authentication_System_API.model
{
    public class PurchaseReturnDetail
    {
        [Key]
        public int PurchaseReturnDetailId { get; set; }

        public int PurchaseReturnRootId { get; set; }
        [ForeignKey("PurchaseReturnRootId")]
        public PurchaseReturnRoot PurchaseReturnRoot { get; set; }

        public int InventoryItemId { get; set; }
        [ForeignKey("InventoryItemId")]
        public InventoryItem InventoryItem { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
