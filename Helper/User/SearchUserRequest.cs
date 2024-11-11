using e_commerce_website.Enums;

namespace e_commerce_website.Helper.User
{
    public class SearchUserRequest
    {
        public string keyWord { get; set; }
        public ActionStatus? status { get; set; }
    }
}
