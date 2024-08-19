using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.FluentValidator;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.BLL.ManageServices;
using TeachEquipManagement.Utilities.CustomAttribute;

namespace TeachEquipManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToolManageController : ControllerBase
    {
        private readonly IToolManageService _toolService;

        public ToolManageController(IToolManageService toolService)
        {
            _toolService = toolService;
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
        public async Task<IActionResult> CreateTool([FromForm] ToolRequest request)
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
        public async Task<IActionResult> UpdateTool([FromForm] ToolUpdateRequest request)
        {
            var validationResult = new ToolUpdateRequestValidator().Validate(request);

            var response = await _toolService.ToolService.Update(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        #endregion

        #region ToolCategory

        [HttpPost]
        [Route("create-tool-category")]
        public async Task<IActionResult> CreateToolCategory([FromBody] ToolCategoryRequest request)
        {
            var validationResult = new ToolCategoryRequestValidator().Validate(request);

            var response = await _toolService.ToolCategoryService.Create(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("all-tool-categories")]
        public async Task<IActionResult> GetAllToolCategories()
        {
            var response = await _toolService.ToolCategoryService.GetAll();

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("tool-category/find")]
        public async Task<IActionResult> FindToolCategoryById([FromBody] ToolCategoryRequest request)
        {
            var response = await _toolService.ToolCategoryService.GetById(request);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("remove-tool-category")]
        public async Task<IActionResult> RemoveToolCategory([FromBody] ToolCategoryRequest request)
        {
            var response = await _toolService.ToolCategoryService.Remove(request);

            return StatusCode(response.StatusCode, response);
        }

        #endregion

        #region Invoice

        [HttpPost]
        [Route("create-invoice")]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoceRequest request)
        {
            var validationResult = new InvoceRequestValidator().Validate(request);

            var response = await _toolService.InvoiceService.Create(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("all-invoices")]
        public async Task<IActionResult> GetAllInvoices()
        {
            var response = await _toolService.InvoiceService.GetAll();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("all-invoice-include-tools")]
        public async Task<IActionResult> GetAllInvoiceIncludeTools()
        {
            var response = await _toolService.InvoiceService.GetAllIncludeTools();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("invoice/find/{id}")]
        public async Task<IActionResult> FindInvoiceById(int id)
        {
            var response = await _toolService.InvoiceService.GetById(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("invoice/latest-invoice/tool/{toolId}")]
        public async Task<IActionResult> GetLatestInvoiceWithToolId(int toolId)
        {
            var response = await _toolService.InvoiceService.GetLatestInvoiceWithToolId(toolId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("invoice-include-tool/latest-invoice/tool/{toolId}")]
        public async Task<IActionResult> GetLatestInvoiceIncludeByToolId(int toolId)
        {
            var response = await _toolService.InvoiceService.GetLatestInvoiceWithTool(toolId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("remove-invoice/{id}")]
        public async Task<IActionResult> RemoveInvoice(int id)
        {
            var response = await _toolService.InvoiceService.Remove(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("update-invoice")]
        public async Task<IActionResult> UpdateInvoice([FromBody] InvoceUpdateRequest request)
        {
            var validationResult = new InvoceUpdateRequestValidator().Validate(request);

            var response = await _toolService.InvoiceService.Update(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        #endregion
    }
}
