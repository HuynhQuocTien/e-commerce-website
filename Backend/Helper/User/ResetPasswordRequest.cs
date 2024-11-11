namespace e_commerce_website.Helper.User
{
    public class ResetPasswordRequest
    {
        public string token { get; set; }
        public string email { get; set; }
        public string newPassword { get; set; }
    }
}
