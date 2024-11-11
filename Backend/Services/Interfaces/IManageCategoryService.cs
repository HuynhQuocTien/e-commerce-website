using e_commerce_website.Helper.Category;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IManageCategoryService
    {
        Task<int> Create(CategoryCreateRequest request);
        Task<int> Update(CategoryUpdateRequest request);
        Task<int> Delete(int providerId);
        Task<CategoryViewModel> getCategoryById(int providerId);
        Task<List<CategoryViewModel>> GetAll();
        Task<CategoryViewModel> GetById(int providerId);
        Task<List<CategoryViewModel>> Search(string search);
    }
}
