using e_commerce_website.Helper.User;
using e_commerce_website.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce_website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUserController : ControllerBase
    {
        private readonly IManageUserService _manageUserService;
        public ManageUserController(IManageUserService manageUserService)
        {
            _manageUserService = manageUserService;
        }
        [HttpGet("GetUserDisplayList")]
        public async Task<IActionResult> GetUserDisplayList()
        {
            var data = await _manageUserService.GetUserDisplayList();
            return Ok(data);
        }
        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(UserChangeStatusRequest request)
        {
            var check = await _manageUserService.ChangeStatusUser(request);
            return Ok(check);
        }
        [HttpPost("DisplayUser")]
        public async Task<IActionResult> DisplayUser(UserChangeStatusRequest request)
        {
            var check = await _manageUserService.ChangeStatusUser(request);
            return Ok(check);
        }
        [HttpPost("SearchUser")]
        public async Task<IActionResult> SearchUser(SearchUserRequest request)
        {
            var data = await _manageUserService.SearchUser(request);
            return Ok(data);
        }
    }
}
