using e_commerce_website.Enums;
using e_commerce_website.Models;

namespace e_commerce_website.ViewModel
{
    public class UserViewModel
    {
        public Guid id { get; set; }
        public string displayname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string? avatar { get; set; }
        public bool gender { get; set; }
        public DateTime birthDay { get; set; }
        public ActionStatus status { get; set; }
        //relative n - 1
        public virtual ICollection<Order> Orders { get; set; }
        //
        public string userType { get; set; }
    }
}
