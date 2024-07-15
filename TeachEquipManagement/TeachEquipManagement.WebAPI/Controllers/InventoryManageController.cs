using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.FluentValidator;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.BLL.ManageServices;
using TeachEquipManagement.BLL.Services;

namespace TeachEquipManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryManageController : ControllerBase
    {
        private readonly IInventoryManageService _inventoryService;

        public InventoryManageController(IInventoryManageService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        #region Inventory

        [HttpPost]
        [Route("create-inventory")]
        public async Task<IActionResult> CreateInventory([FromBody] InventoryRequest request)
        {
            var validationResult = new InventoryRequestValidator().Validate(request);

            var response = await _inventoryService.InventoryService.Create(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("all-inventories")]
        public async Task<IActionResult> GetAllInventories()
        {
            var response = await _inventoryService.InventoryService.GetAll();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("inventory/find/{id}")]
        public async Task<IActionResult> FindInventoryById(Guid id)
        {
            var response = await _inventoryService.InventoryService.GetById(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("remove-inventory/{id}")]
        public async Task<IActionResult> RemoveInventory(Guid id)
        {
            var response = await _inventoryService.InventoryService.Remove(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("update-inventory")]
        public async Task<IActionResult> UpdateInventory([FromBody] InventoryUpdateRequest request)
        {
            var validationResult = new InventoryUpdateRequestValidator().Validate(request);

            var response = await _inventoryService.InventoryService.Update(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        #endregion

        #region Enum Type

        [HttpGet]
        [Route("approval-enums")]
        public  IActionResult GetAllApprovalEnums()
        {
            var response = _inventoryService.ApprovalRequestService.GetListApprovalEnum();

            return Ok(response);
        }

        [HttpGet]
        [Route("request-type-enums")]
        public IActionResult GetAllRequestTypeEnums()
        {
            var response = _inventoryService.ApprovalRequestService.GetListRequestTypeEnum();

            return Ok(response);
        }

        #endregion

        #region ApprovalRequest

        #endregion
    }
}
