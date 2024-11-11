using e_commerce_website.Helper.Provider;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IManageProviderService
    {
        Task<int> Create(ProviderCreateRequest request);
        Task<int> Update(ProviderUpdateRequest request);
        Task<int> Delete(int categoryId);
        Task<ProviderViewModel> getProviderById(int categoryId);
        Task<List<ProviderViewModel>> GetAll();
        Task<List<ProviderViewModel>> Search(string search);
    }
}
