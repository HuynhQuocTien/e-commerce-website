using e_commerce_website.Helper.Product;
using e_commerce_website.Helper;
using e_commerce_website.Services.Interfaces;
using e_commerce_website.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce_website.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        //// GET: api/<ProductController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ProductController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ProductController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ProductController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProductController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        
        //http://localhost:port/product/products-paging
        [HttpGet("products-paging")]
        public async Task<IActionResult> getProductByCategoryId([FromQuery] GetProductPagingRequest request)
        {
            var products = await _productService.GetAllByCategoryId(request);
            return Ok(products);
        }
        [HttpGet("get-all-products/{itemCount}")]
        public async Task<IActionResult> getAllProduct(int itemCount)
        {
            var products = await _productService.GetAllProduct(itemCount);
            return Ok(new { data = products.products, totalColumns = products.totalColumns });
        }
        [HttpGet("products-top-view-count/{all}")]
        public async Task<IActionResult> getTopViewCountProduct(bool all = false)
        {
            var products = await _productService.GetTopViewCountProduct(all);
            return Ok(products);
        }
        [HttpGet("get-product-by-id/{productId}")]
        public async Task<IActionResult> getProductById(int productId)
        {
            var product = await _productService.getProductById(productId);
            return Ok(product);
        }
        [HttpGet("search-products")]
        public async Task<IActionResult> searchProducts([FromQuery] SearchProductRequest request)
        {
            List<ProductViewModel> products = await _productService.SearchProducts(request);
            List<CategoryViewModel> catetegories = null;
            if (products != null)
            {
                catetegories = await _productService.getListCategoryByGeneralityName(products[0].category.generalityName);
            }
            return Ok(new { products = products, categories = catetegories == null ? null : catetegories });
        }
        [HttpGet("get-listcategory-by-generalityname/{generalityName}")]
        public async Task<IActionResult> getListCategoryByGeneralityName(string generalityName)
        {
            var listCategory = await _productService.getListCategoryByGeneralityName(generalityName);
            return Ok(listCategory);
        }
        [HttpPost("Paging")]
        public async Task<IActionResult> Paging(ProductPagingRequest request)
        {
            var data = await _productService.Paging(request);
            return Ok(data);
        }
    }
}
