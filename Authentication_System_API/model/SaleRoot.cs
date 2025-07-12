using System.ComponentModel.DataAnnotations;

namespace Authentication_System_API.model
{
    public class SaleRoot
    {
        [Key]
        public int SaleRootId { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
