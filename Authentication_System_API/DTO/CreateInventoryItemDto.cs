namespace Authentication_System_API.DTO
{
    public class CreateInventoryItemDto
    {
        public string ItemName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int ItemCategoryId { get; set; }

        public int UnitId { get; set; }
        public IFormFile? FilePath { get; set; }
    }
}
