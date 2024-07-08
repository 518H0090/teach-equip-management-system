using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.FluentValidator;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.BLL.ManageServices;
using TeachEquipManagement.BLL.Services;

namespace TeachEquipManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManageController : ControllerBase
    {
        private readonly IUserManageService _userManageService;
        private readonly IGraphService _graphService;

        public UserManageController(IUserManageService userManageService, IGraphService graphService)
        {
            _userManageService = userManageService;
            _graphService = graphService;
        }

        #region User

        [HttpPost]
        [Route("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            var validationResult = new UserRequestValidator().Validate(request);

            var response = await _userManageService.UserService.CreateUser(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        #endregion
    }
}
