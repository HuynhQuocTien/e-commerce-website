using e_commerce_website.Helper.User;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IManageUserService
    {
        Task<List<UserViewModel>> GetUserDisplayList();
        Task<bool> ChangeStatusUser(UserChangeStatusRequest request);
        Task<List<UserViewModel>> SearchUser(SearchUserRequest request);
    }
}
