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

        #region Supplier

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
        [Route("all-supplier-include-tool")]
        public async Task<IActionResult> GetAllSupplierIncludeTools()
        {
            var response = await _toolService.SupplierService.GetAllIncludeTools();

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

        #endregion

        #region Category

        [HttpPost]
        [Route("create-category")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            var validationResult = new CategoryRequestValidator().Validate(request);

            var response = await _toolService.CategoryService.Create(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _toolService.CategoryService.GetAll();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("category/find/{id}")]
        public async Task<IActionResult> FindCategoryById(int id)
        {
            var response = await _toolService.CategoryService.GetById(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("remove-category/{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var response = await _toolService.CategoryService.Remove(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("update-category")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateRequest request)
        {
            var validationResult = new CategoryUpdateRequestValidator().Validate(request);

            var response = await _toolService.CategoryService.Update(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        #endregion

        #region Tool

        [HttpPost]
        [Route("create-tool")]
        public async Task<IActionResult> CreateTool([FromBody] ToolRequest request)
        {
            var validationResult = new ToolRequestValidator().Validate(request);

            var response = await _toolService.ToolService.Create(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("all-tools")]
        public async Task<IActionResult> GetAllTools()
        {
            var response = await _toolService.ToolService.GetAll();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("all-tools-include-supplier")]
        public async Task<IActionResult> GetAllToolsIncludeSupplier()
        {
            var response = await _toolService.ToolService.GetAllIncludeSupplier();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("tool/find/{id}")]
        public async Task<IActionResult> FindToolById(int id)
        {
            var response = await _toolService.ToolService.GetById(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("remove-tool/{id}")]
        public async Task<IActionResult> RemoveTool(int id)
        {
            var response = await _toolService.ToolService.Remove(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("update-tool")]
        public async Task<IActionResult> UpdateTool([FromBody] ToolUpdateRequest request)
        {
            var validationResult = new ToolUpdateRequestValidator().Validate(request);

            var response = await _toolService.ToolService.Update(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        #endregion
    }
}
