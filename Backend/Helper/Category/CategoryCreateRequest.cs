using e_commerce_website.Enums;

namespace e_commerce_website.Helper.Category
{
    public class CategoryCreateRequest
    {
        public string generalityName { get; set; }
        //áo sơ mi // quần tây

        public string name { get; set; }

        public ActionStatus status { get; set; }
    }
}
