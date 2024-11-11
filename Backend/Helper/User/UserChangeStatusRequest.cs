using e_commerce_website.Enums;

namespace e_commerce_website.Helper.User
{
    public class UserChangeStatusRequest
    {
        public Guid id { get; set; }
        public ActionStatus status { get; set; }
    }
}
