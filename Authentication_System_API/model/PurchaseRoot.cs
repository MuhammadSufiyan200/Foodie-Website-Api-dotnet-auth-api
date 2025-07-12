using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication_System_API.model
{
    public class PurchaseRoot
    {
        [Key]
        public int PurchaseRootId { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
