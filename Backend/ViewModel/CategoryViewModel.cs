using e_commerce_website.Enums;
using e_commerce_website.Models;

namespace e_commerce_website.ViewModel
{
    public class CategoryViewModel
    {
        public int id { get; set; }
        // quần áo

        public string generalityName { get; set; }
        //áo sơ mi // quần tây

        public string name { get; set; }

        public ActionStatus status { get; set; }
        //relative n - 1
        public virtual ICollection<Product> Products { get; set; }
    }
}
