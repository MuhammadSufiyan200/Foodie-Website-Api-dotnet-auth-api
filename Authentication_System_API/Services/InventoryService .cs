using Authentication_System_API.Data;
using Authentication_System_API.DTO;
using Authentication_System_API.model;
using Microsoft.EntityFrameworkCore;

namespace Authentication_System_API.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ApplictionDbContext _db;
        private readonly IWebHostEnvironment _env;

        public InventoryService(ApplictionDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        //------------------ ItemCategory ------------------
        public async Task<ItemCategory> CreateItemCategoryAsync(CreateItemCategoryDto dto)
        {
            var itemCategory = new ItemCategory
            {
                CategoryName = dto.CategoryName
            };

            _db.Tbl_ItemCategories.Add(itemCategory);
            await _db.SaveChangesAsync();

            return itemCategory;
        }
        public async Task<List<ItemCategory>> GetAllItemCategoryAsync()
        {
            return await _db.Tbl_ItemCategories.ToListAsync();
        }
        public async Task<ItemCategory?> GetDatabyIDItemCategoryAsync(int id)
        {
            return await _db.Tbl_ItemCategories.FindAsync(id);
        }
        public async Task<bool> UpdateItemCategoryAsync(int id, CreateItemCategoryDto dto)
        {
            var existingCategory = await _db.Tbl_ItemCategories.FindAsync(id);
            if (existingCategory == null)
                return false;

            existingCategory.CategoryName = dto.CategoryName;
            await _db.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteItemCategoryAsync(int id)
        {
            var category = await _db.Tbl_ItemCategories.FindAsync(id);
            if (category == null)
                return false;

            _db.Tbl_ItemCategories.Remove(category);
            await _db.SaveChangesAsync();

            return true;
        }

        //------------------ End ------------------//
        //------------------ Inventory Item ------------------

        //public async Task<InventoryItem> CreateInventoryItemAsync(CreateInventoryItemDto dto)
        //{
        //    var inventoryItem = new InventoryItem
        //    {
        //        ItemName = dto.ItemName,
        //        Price = dto.Price,
        //        StockQuantity = dto.StockQuantity,
        //        ItemCategoryId = dto.ItemCategoryId,
        //        UnitId = dto.UnitId
        //    };
        //    if (dto.FilePath != null && dto.FilePath.Length > 0)
        //    {
        //        var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Image");
        //        if (!Directory.Exists(folderpath))
        //            Directory.CreateDirectory(folderpath);

        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.FilePath.FileName);
        //        var filePath = Path.Combine(folderpath, fileName);

        //        using (var Stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await dto.FilePath.CopyToAsync(Stream);
        //        }

        //        inventoryItem.ImagePath = Path.Combine("Image", fileName).Replace("\\", "/");
        //    }

        //    _db.Tbl_InventoryItems.Add(inventoryItem);
        //    await _db.SaveChangesAsync();

        //    var stockEntry = new StockLedger
        //    {
        //        InventoryItemId = inventoryItem.InventoryItemId,
        //        TransactionDate = DateTime.Now,
        //        TransactionType = "I",
        //        Quantity = dto.StockQuantity,
        //        Rate = dto.Price,
        //        Remarks = "Item added on: " + DateTime.Now.ToString("yyyy - MM - dd HH: mm: ss")
        //    };

        //    _db.StockLedger.Add(stockEntry);
        //    await _db.SaveChangesAsync();


        //    return inventoryItem;
        //}
        public async Task<InventoryItem> CreateInventoryItemAsync(CreateInventoryItemDto dto)
        {
            var inventoryItem = new InventoryItem
            {
                ItemName = dto.ItemName,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                ItemCategoryId = dto.ItemCategoryId,
                UnitId = dto.UnitId
            };

            if (dto.FilePath != null && dto.FilePath.Length > 0)
            {
                var folderPath = Path.Combine(_env.WebRootPath, "Image"); // Using _env.WebRootPath
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.FilePath.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.FilePath.CopyToAsync(stream);
                }

                inventoryItem.ImagePath = Path.Combine("Image", fileName).Replace("\\", "/");
            }

            _db.Tbl_InventoryItems.Add(inventoryItem);
            await _db.SaveChangesAsync();

            var stockEntry = new StockLedger
            {
                InventoryItemId = inventoryItem.InventoryItemId,
                TransactionDate = DateTime.Now,
                TransactionType = "I", // "I" stands for item in
                Quantity = dto.StockQuantity,
                Rate = dto.Price,
                Remarks = "Item added on: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            _db.StockLedger.Add(stockEntry);
            await _db.SaveChangesAsync();

            return inventoryItem;
        }

        public async Task<List<GetInventoryItemDto>> GetAllInventoryItemsAsync()
        {
            return await _db.Tbl_InventoryItems
               .Include(x => x.ItemCategory)
               .Include(x => x.Unit)
               .Select(item => new GetInventoryItemDto
               {
                   Id = item.InventoryItemId,
                   ItemName = item.ItemName,
                   Price = item.Price,
                   StockQuantity = item.StockQuantity,
                   Img = item.ImagePath,
                   CategoryName = item.ItemCategory.CategoryName,
                   UnitName = item.Unit.UnitName
               })
               .ToListAsync();
        }
        public async Task<InventoryItem?> GetInventoryItemByIdAsync(int id)
        {
            return await _db.Tbl_InventoryItems
                        .Include(x => x.ItemCategory)
                        .Include(x => x.Unit)
                        .FirstOrDefaultAsync(x => x.InventoryItemId == id);
        }
        public async Task<bool> UpdateInventoryItemAsync(int id, CreateInventoryItemDto dto)
        {
            var existingItem = await _db.Tbl_InventoryItems.FindAsync(id);
            if (existingItem == null)
                return false;

            existingItem.ItemName = dto.ItemName;
            existingItem.Price = dto.Price;
            existingItem.StockQuantity = dto.StockQuantity;
            existingItem.ItemCategoryId = dto.ItemCategoryId;
            existingItem.UnitId = dto.UnitId;

            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteInventoryItemAsync(int id)
        {
            var item = await _db.Tbl_InventoryItems.FindAsync(id);
            if (item == null)
                return false;

            _db.Tbl_InventoryItems.Remove(item);
            await _db.SaveChangesAsync();
            return true;
        }

        //------------------ End ------------------//
        //------------------ Unit ------------------

        public async Task<Unit> CreateUnitAsync(CreateUnitDTO dto)
        {
            var Unit = new Unit
            {
                UnitName = dto.UnitName,
                
            };

            _db.Tbl_Units.Add(Unit);
            await _db.SaveChangesAsync();
            return Unit;
        }
        public async Task<List<Unit>> GetAllUnitAsync()
        {
            return await _db.Tbl_Units.ToListAsync();
        }
        public async Task<Unit?> GetUnitByIdAsync(int id)
        {
            return await _db.Tbl_Units.FindAsync(id);
        }
        public async Task<bool> UpdateUnitAsync(int id, CreateUnitDTO dto)
        {
            var existingItem = await _db.Tbl_Units.FindAsync(id);
            if (existingItem == null)
                return false;

            existingItem.UnitName = dto.UnitName;
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUnitAsync(int id)
        {
            var item = await _db.Tbl_Units.FindAsync(id);
            if (item == null)
                return false;

            _db.Tbl_Units.Remove(item);
            await _db.SaveChangesAsync();
            return true;
        }

        //------------------ End ------------------//




    }

}
