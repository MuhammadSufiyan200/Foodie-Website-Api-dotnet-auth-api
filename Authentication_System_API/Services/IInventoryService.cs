using Authentication_System_API.DTO;
using Authentication_System_API.model;

namespace Authentication_System_API.Services
{
    public interface IInventoryService
    {
        //-------------ItemCategory------------
        Task<ItemCategory> CreateItemCategoryAsync(CreateItemCategoryDto dto);
        Task<List<ItemCategory>> GetAllItemCategoryAsync();
        Task<ItemCategory?> GetDatabyIDItemCategoryAsync(int id);
        Task<bool> UpdateItemCategoryAsync(int id,CreateItemCategoryDto dto);
        Task<bool> DeleteItemCategoryAsync(int id);

        //-----------------InventoryItem-------------------
        Task<InventoryItem> CreateInventoryItemAsync(CreateInventoryItemDto dto);
        Task<List<GetInventoryItemDto>> GetAllInventoryItemsAsync();
        Task<InventoryItem?> GetInventoryItemByIdAsync(int id);
        Task<bool> UpdateInventoryItemAsync(int id, CreateInventoryItemDto dto);
        Task<bool> DeleteInventoryItemAsync(int id);

        //-----------------Unit-------------------
        Task<Unit> CreateUnitAsync( CreateUnitDTO dto);
        Task<List<Unit>> GetAllUnitAsync();
        Task<Unit?> GetUnitByIdAsync(int id);
        Task<bool> UpdateUnitAsync(int id, CreateUnitDTO dto);
        Task<bool> DeleteUnitAsync(int id);



    }
}
