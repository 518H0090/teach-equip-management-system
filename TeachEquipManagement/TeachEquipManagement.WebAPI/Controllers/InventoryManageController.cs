using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.BLL.ManageServices;

namespace TeachEquipManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryManageController : ControllerBase
    {
        private readonly IManageService _manageService;
        private readonly IGraphService _graphService;

        public InventoryManageController(IManageService manageService, IGraphService graphService)
        {
            _manageService = manageService;
            _graphService = graphService;
        }
    }
}
