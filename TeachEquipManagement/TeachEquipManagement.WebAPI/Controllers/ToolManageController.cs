using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
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
        public async Task<IActionResult> CreateSupplier(SupplierRequest request)
        {
            var response = await _toolService.SupplierService.Create(request);

            if (response.StatusCode == StatusCodes.Status201Created)
            {
                return Created("CreateNewRestaurant", response);
            }

            else if (response.StatusCode == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response.Message);
            }

            return Ok("Oke");
        }
    }
}
