namespace e_commerce_website.Helper.User
{
    public class UserUpdateRequest
    {
        public Guid id { get; set; }
        public string displayname { get; set; }
        public string? phone { get; set; }
        public IFormFile? file { get; set; }
        public string? avatar { get; set; }
        public string address { get; set; }
        public bool gender { get; set; }
        public DateTime birthDay { get; set; }
    }
}
