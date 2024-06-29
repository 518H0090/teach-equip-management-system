using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeachEquipManagement.BLL.IServices;

namespace TeachEquipManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGraphService _graphService;

        public AuthenController(IUserService userService, IGraphService graphService)
        {
            _userService = userService;
            _graphService = graphService;
        }

        [HttpGet]
        [Route("authen-getall")]
        public async Task<IActionResult> AuthenticationGetAll()
        {
            _graphService.Test();
            return Ok(await _userService.ToiDayD());
        }
    }
}
