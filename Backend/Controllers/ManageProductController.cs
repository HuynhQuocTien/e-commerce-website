using e_commerce_website.Helper.Product;
using e_commerce_website.Services.Interfaces;
using e_commerce_website.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce_website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageProductController : ControllerBase
    {
        public readonly IManageProductService _manageProductService;
        public ManageProductController(IManageProductService manageProductService)
        {
            _manageProductService = manageProductService;
        }
        [HttpGet]
        [Authorize(Policy  = "Admin")]
        public async Task<IActionResult> getAll()
        {
            var data = await _manageProductService.GetAll();
            return Ok(data);
        }
        [HttpGet("getProductById")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getProductById(int productId)
        {
            var product = await _manageProductService.getProductById(productId);
            return Ok(product);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> create([FromForm] ProductCreateRequest request)
        {

            var productId = await _manageProductService.Create(request);
            if (productId == 0)
            {
                return BadRequest();
            }
            var product = await _manageProductService.getProductById(productId);
            return CreatedAtAction(nameof(getProductById), new { id = productId }, product);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> update([FromForm] ProductUpdateRequest request)
        {

            var productId = await _manageProductService.Update(request);
            if (productId == 0)
            {
                return BadRequest();

            }
            var product = await _manageProductService.getProductById(productId);
            return Ok(new { message = "Cập nhập sản phẩm thành công!", product });
        }
        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> delete(int productId)
        {
            var result = await _manageProductService.Delete(productId);
            if (result > 0)
            {
                return Ok(new { message = "Xóa sản phẩm thành công!" });
            }
            return BadRequest("Xóa sản phẩm thất bại!");
        }
        [HttpPost("search")]
        public async Task<IActionResult> search([FromBody] ProductSearchRequest request)
        {
            var data = await _manageProductService.searchProduct(request);
            return Ok(data);
        }
        [HttpPost("uploadImage")]

        public async Task<IActionResult> uploadImage(IFormFile file)
        {
            var urlImage = await _manageProductService.SaveFile(file);
            if (urlImage == null)
            {
                return BadRequest("Upload ko thành công!");
            }
            return Ok(new
            {
                name = "image",
                status = "done",
                url = urlImage
            });
        }
        [HttpPost("image")]

        public IActionResult upload()
        {
            return Ok(new
            {
                name = "image",
                status = "done",
                url = "hihi"
            });
        }
    }
}
