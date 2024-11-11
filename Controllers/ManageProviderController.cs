using e_commerce_website.Helper.Provider;
using e_commerce_website.Services.Interfaces;
using e_commerce_website.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce_website.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ManageProviderController : ControllerBase
    {
        private readonly IManageProviderService _manageProviderService;
        public ManageProviderController(IManageProviderService manageProviderService)
        {
            _manageProviderService = manageProviderService;
        }
        //// GET: api/<ManageProviderController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ManageProviderController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ManageProviderController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ManageProviderController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ManageProviderController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        [HttpGet]
        public async Task<List<ProviderViewModel>> getAll()
        {
            var data = await _manageProviderService.GetAll();
            return data;
        }
        [HttpGet("search/{search}")]
        public async Task<List<ProviderViewModel>> search(string search)
        {
            var data = await _manageProviderService.Search(search);
            return data;
        }
        [HttpGet("getProviderById")]
        public async Task<IActionResult> getProviderById(int providerId)
        {
            var provider = await _manageProviderService.getProviderById(providerId);
            return Ok(provider);
        }
        [HttpPost]
        public async Task<IActionResult> create(ProviderCreateRequest request)
        {
            var providerId = await _manageProviderService.Create(request);
            if (providerId == 0)
            {
                return BadRequest();
            }
            var provider = await _manageProviderService.getProviderById(providerId);
            return CreatedAtAction(nameof(getProviderById), new { id = providerId }, provider);
        }
        [HttpPut]
        public async Task<IActionResult> update(ProviderUpdateRequest request)
        {
            var result = await _manageProviderService.Update(request);
            if (result > 0)
            {
                return Ok(new { message = "Cập nhập nhà cung ứng thành công!" });
            }
            return BadRequest("Cập nhập nhà cung ứng thất bại!");
        }
        [HttpDelete("{providerId}")]
        public async Task<IActionResult> delete(int providerId)
        {
            var result = await _manageProviderService.Delete(providerId);
            if (result > 0)
            {
                return Ok(new { message = "Xóa nhà cung ứng thành công!" });
            }
            return BadRequest("Xóa nhà cung ứng thất bại!");
        }
    }
}
