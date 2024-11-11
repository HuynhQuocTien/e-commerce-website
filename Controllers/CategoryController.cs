using e_commerce_website.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce_website.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IManageCategoryService _manageCategoryService;
        public CategoryController(IManageCategoryService manageCategoryService)
        {
            _manageCategoryService = manageCategoryService;
        }
        [HttpGet("getAllCategory")]
        public async Task<IActionResult> getCategory()
        {
            var categories = await _manageCategoryService.GetAll();
            return Ok(categories);
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CategoryController>/5
        [HttpGet("getCategoryById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var categories = await _manageCategoryService.GetById(id);

            return Ok(categories);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
