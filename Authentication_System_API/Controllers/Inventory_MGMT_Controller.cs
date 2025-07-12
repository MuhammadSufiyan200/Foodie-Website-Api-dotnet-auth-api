using Authentication_System_API.Data;
using Authentication_System_API.DTO;
using Authentication_System_API.model;
using Authentication_System_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Inventory_MGMT_Controller : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public Inventory_MGMT_Controller(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // -------------------- Item Category --------------------
        [HttpPost("CreateItemCategory")]
        public async Task<IActionResult> CreateItemCategory([FromBody] CreateItemCategoryDto dto)
        {   
            var result = await _inventoryService.CreateItemCategoryAsync(dto);
            return Ok(result);
        }

        [HttpGet("GetAllItemCategories")]
        public async Task<IActionResult> GetAllItemCategories()
        {
            var result = await _inventoryService.GetAllItemCategoryAsync();
            return Ok(result);
        }
        [HttpGet("GetItemCategoryById/{id}")]
        public async Task<IActionResult> GetItemCategoryById(int id)
        {
            var result = await _inventoryService.GetDatabyIDItemCategoryAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPut("UpdateItemCategory/{id}")]
        public async Task<IActionResult> UpdateItemCategory(int id, [FromBody] CreateItemCategoryDto dto)
        {
            var isUpdated = await _inventoryService.UpdateItemCategoryAsync(id, dto);
            if (!isUpdated)
                return NotFound();

            return Ok(new { Message = "Category Updated Successfully" });
        }
        [HttpDelete("DeleteItemCategory/{id}")]
        public async Task<IActionResult> DeleteItemCategory(int id)
        {
            var isDeleted = await _inventoryService.DeleteItemCategoryAsync(id);
            if (!isDeleted)
                return NotFound();

            return Ok(new { Message = "Category Deleted Successfully" });
        }
        
        // -------------------- InventoryItem --------------------
        [HttpPost("CreateInventoryItem")]
        public async Task<IActionResult> CreateInventoryItem([FromForm] CreateInventoryItemDto dto)
        {
            var result = await _inventoryService.CreateInventoryItemAsync(dto);
            return Ok(result);
        }
        [HttpGet("GetAllInventoryItem")]
        public async Task<IActionResult> GetAllInventoryItem()
        {
            var result = await _inventoryService.GetAllInventoryItemsAsync();
            return Ok(result);
        }
        [HttpGet("GetInventoryItemById/{id}")]
        public async Task<IActionResult> GetInventoryItemById(int id)
        {
            var result = await _inventoryService.GetInventoryItemByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPut("UpdateInventoryItem/{id}")]
        public async Task<IActionResult> UpdateInventoryItem(int id, [FromBody] CreateInventoryItemDto dto)
        {
            var isUpdated = await _inventoryService.UpdateInventoryItemAsync(id, dto);
            if (!isUpdated)
                return NotFound();

            return Ok(new { Message = "Category Updated Successfully" });
        }
        [HttpDelete("DeleteInventoryItem/{id}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var isDeleted = await _inventoryService.DeleteInventoryItemAsync(id);
            if (!isDeleted)
                return NotFound();

            return Ok(new { Message = "Category Deleted Successfully" });
        }

        // -------------------- Unit --------------------

        [HttpPost("CreateUnitDTO")]
        public async Task<IActionResult> CreateUnitDTO([FromBody] CreateUnitDTO dto)
        {
            var result = await _inventoryService.CreateUnitAsync(dto);
            return Ok(result);
        }
        [HttpGet("GetAllUnit")]
        public async Task<IActionResult> GetAllUnit()
        {
            var result = await _inventoryService.GetAllUnitAsync();
            return Ok(result);
        }
        [HttpGet("GetUnitById/{id}")]
        public async Task<IActionResult> GetUnitById(int id)
        {
            var result = await _inventoryService.GetUnitByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPut("UpdateUnit/{id}")]
        public async Task<IActionResult> UpdateUnit(int id, [FromBody] CreateUnitDTO dto)
        {
            var isUpdated = await _inventoryService.UpdateUnitAsync(id, dto);
            if (!isUpdated)
                return NotFound();

            return Ok(new { Message = "Category Updated Successfully" });
        }
        [HttpDelete("DeleteUnit/{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var isDeleted = await _inventoryService.DeleteUnitAsync(id);
            if (!isDeleted)
                return NotFound();

            return Ok(new { Message = "Category Deleted Successfully" });
        }
        //------------End-----------//
    }
}
