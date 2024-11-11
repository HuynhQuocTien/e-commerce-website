

using e_commerce_website.Helper.Facebook;
using e_commerce_website.Helper.User;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> Authenticate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
        Task<UserViewModel> getUserById(Guid userId);
        Task<Guid> Update(UserUpdateRequest request);
        Task<bool> ForgotPassword(ForgotPasswordRequest request);
        Task<bool> ResetPassword(ResetPasswordRequest request);
        Task<string> LoginWithFacebook(FacebookLoginRequest request);
    }
}
