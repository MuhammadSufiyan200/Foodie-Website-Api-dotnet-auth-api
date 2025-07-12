using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication_System_API.model
{
    public class StockLedger
    {
        [Key]
        public int Id { get; set; }

        public int InventoryItemId { get; set; }
        [ForeignKey("InventoryItemId")]
        public InventoryItem Item { get; set; }

        public string TransactionType { get; set; } // "Purchase", "Sale", etc.

        public int Quantity { get; set; }

        public decimal Rate { get; set; } // Per unit rate

        public DateTime TransactionDate { get; set; }

        public string Remarks { get; set; }

    }
}
