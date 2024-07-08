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

        public InventoryManageController(IManageService manageService)
        {
            _manageService = manageService;
        }
    }
}
