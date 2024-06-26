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

        public AuthenController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("authen-getall")]
        public async Task<IActionResult> AuthenticationGetAll()
        {
            return Ok(await _userService.ToiDayD());
        }
    }
}
