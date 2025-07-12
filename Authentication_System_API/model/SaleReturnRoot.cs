using System.ComponentModel.DataAnnotations;

namespace Authentication_System_API.model
{
    public class SaleReturnRoot
    {
        [Key]
        public int SaleReturnRootId { get; set; }

        public DateTime ReturnDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public ICollection<SaleReturnDetail> SaleReturnDetails { get; set; }
    }
}
