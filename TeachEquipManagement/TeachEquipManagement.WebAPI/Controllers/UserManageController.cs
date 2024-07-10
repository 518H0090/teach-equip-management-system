using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.FluentValidator;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.BLL.ManageServices;
using TeachEquipManagement.BLL.Services;
using TeachEquipManagement.Utilities.OptionPattern;

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

        [HttpGet]
        [Route("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userManageService.UserService.GetAllUser();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("user/find/{id}")]
        public async Task<IActionResult> FindUserById(Guid id)
        {
            var response = await _userManageService.UserService.GetUserById(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            var validationResult = new UserUpdateRequestValidator().Validate(request);

            var response = await _userManageService.UserService.UpdateUser(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("remove-user/{id}")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            var response = await _userManageService.UserService.RemoveUser(id);

            return StatusCode(response.StatusCode, response);
        }

        #endregion

        #region Token

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticatedRequest request)
        {
            var validationResult = new AuthenticatedRequestValidator().Validate(request);

            var response = await _userManageService.TokenService.Login(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] AuthenticatedRefreshRequest request)
        {
            var validationResult = new AuthenticatedRefreshRequestValidator().Validate(request);

            var response = await _userManageService.TokenService.Refresh(request, validationResult);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("revoke-token")]
        public async Task<IActionResult> Revoke([FromBody] AuthenticatedRefreshRequest request)
        {
            var validationResult = new AuthenticatedRefreshRequestValidator().Validate(request);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _userManageService.TokenService.Revoke(request.AccessToken);

            return StatusCode(response.StatusCode, response);
        }

        #endregion
    }
}
