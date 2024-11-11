using e_commerce_website.Helper.Category;
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
    public class ManageCategoryController : ControllerBase
    {
        private readonly IManageCategoryService _manageCategoryService;
        public ManageCategoryController(IManageCategoryService manageCategoryService)
        {
            _manageCategoryService = manageCategoryService;
        }
        //// GET: api/<ManageCategoryController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ManageCategoryController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ManageCategoryController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ManageCategoryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ManageCategoryController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        [HttpGet]

        public async Task<List<CategoryViewModel>> getAll()
        {
            var data = await _manageCategoryService.GetAll();
            return data;
        }
        [HttpGet("search/{search}")]
        public async Task<List<CategoryViewModel>> search(string search)
        {
            var data = await _manageCategoryService.Search(search);
            return data;
        }
        [HttpGet("getCategoryById")]
        public async Task<IActionResult> getCategoryById(int categoryId)
        {
            var category = await _manageCategoryService.getCategoryById(categoryId);
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> create(CategoryCreateRequest request)
        {
            var categoryId = await _manageCategoryService.Create(request);
            if (categoryId == 0)
            {
                return BadRequest();
            }
            var category = await _manageCategoryService.getCategoryById(categoryId);
            return CreatedAtAction(nameof(getCategoryById), new { id = categoryId }, category);
        }
        [HttpPut]
        public async Task<IActionResult> update(CategoryUpdateRequest request)
        {
            var result = await _manageCategoryService.Update(request);
            if (result > 0)
            {
                return Ok(new { message = "Cập nhập danh mục thành công!" });
            }
            return BadRequest("Cập nhập danh mục thất bại!");
        }
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> delete(int categoryId)
        {
            var result = await _manageCategoryService.Delete(categoryId);
            if (result > 0)
            {
                return Ok(new { message = "Xóa danh mục thành công!" });
            }
            return BadRequest("Xóa danh mục thất bại!");
        }
    }
}
