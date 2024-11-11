using e_commerce_website.Helper.User;
using e_commerce_website.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce_website.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ManageUserController : ControllerBase
    {
        private readonly IManageUserService _manageUserService;
        public ManageUserController(IManageUserService manageUserService)
        {
            _manageUserService = manageUserService;
        }
        //// GET: api/<ManageUserController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ManageUserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ManageUserController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ManageUserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ManageUserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
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
