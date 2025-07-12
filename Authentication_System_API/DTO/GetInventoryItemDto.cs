namespace Authentication_System_API.DTO
{
    public class GetInventoryItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public string Img { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
    }
}
