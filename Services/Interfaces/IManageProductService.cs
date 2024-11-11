using e_commerce_website.Helper.Product;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest product);
        Task<int> Update(ProductUpdateRequest product);
        Task<int> Delete(int productId);
        Task<ProductViewModel> getProductById(int productId);
        Task<List<ProductViewModel>> GetAll();
        //page view model (list , total record)
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
        Task<List<ProductViewModel>> searchProduct(ProductSearchRequest request);
        Task<string> SaveFile(IFormFile file);
    }
}
