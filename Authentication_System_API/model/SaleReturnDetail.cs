using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Authentication_System_API.model
{
    public class SaleReturnDetail
    {
        [Key]
        public int SaleReturnDetailId { get; set; }

        public int SaleReturnRootId { get; set; }
        [ForeignKey("SaleReturnRootId")]
        public SaleReturnRoot SaleReturnRoot { get; set; }

        public int InventoryItemId { get; set; }
        [ForeignKey("InventoryItemId")]
        public InventoryItem InventoryItem { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
