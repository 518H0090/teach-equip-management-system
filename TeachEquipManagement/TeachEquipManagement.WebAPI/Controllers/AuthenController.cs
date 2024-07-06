using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.BLL.ManageServices;

namespace TeachEquipManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IManageService _manageService;
        private readonly IGraphService _graphService;

        public AuthenController(IManageService manageService, IGraphService graphService)
        {
            _manageService = manageService;
            _graphService = graphService;
        }

        [HttpGet]
        [Route("authen-getall")]
        public async Task<IActionResult> AuthenticationGetAll()
        {
            await _graphService.GetSharePointDataAsync();
            return Ok();
        }
    }
}
