using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.FluentValidator;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.BLL.ManageServices;

namespace TeachEquipManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolManageController : ControllerBase
    {
        private readonly IToolManageService _toolService;
        private readonly IGraphService _graphService;

        public ToolManageController(IToolManageService toolService, IGraphService graphService)
        {
            _toolService = toolService;
            _graphService = graphService;
        }

        [HttpPost]
        [Route("create-supplier")]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierRequest request)
        {
            var validationResult = new SupplierRequestValidator().Validate(request);

            var response = await _toolService.SupplierService.Create(request, validationResult);

            return StatusCode(response.StatusCode, response); 
        }

        [HttpGet]
        [Route("all-supplier")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var response = await _toolService.SupplierService.GetAll();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("supplier/find/{id}")]
        public async Task<IActionResult> FindSupplierById(int id)
        {
            var response = await _toolService.SupplierService.GetById(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("remove-supplier/{id}")]
        public async Task<IActionResult> RemoveSupplier(int id)
        {
            var response = await _toolService.SupplierService.Remove(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("update-supplier")]
        public async Task<IActionResult> UpdateSupplier([FromBody] SupplierUpdateRequest request)
        {
            var validationResult = new SupplierUpdateRequestValidator().Validate(request);

            var response = await _toolService.SupplierService.Update(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

    }
}
