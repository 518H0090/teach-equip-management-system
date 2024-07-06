using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.BLL.ManageServices;

namespace TeachEquipManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolManageController : ControllerBase
    {
        private readonly IManageService _manageService;
        private readonly IGraphService _graphService;

        public ToolManageController(IManageService manageService, IGraphService graphService)
        {
            _manageService = manageService;
            _graphService = graphService;
        }
    }
}
