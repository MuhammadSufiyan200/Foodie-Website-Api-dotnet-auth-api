using System.ComponentModel.DataAnnotations;

namespace Authentication_System_API.model
{
    public class PurchaseReturnRoot
    {
        [Key]
        public int PurchaseReturnRootId { get; set; }

        public DateTime ReturnDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public ICollection<PurchaseReturnDetail> PurchaseReturnDetails { get; set; }
    }
}
