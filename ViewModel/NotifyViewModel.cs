using e_commerce_website.Enums;
using System.ComponentModel;

namespace e_commerce_website.ViewModel
{
    public class NotifyViewModel
    {
        public string notify { get; set; }
        public string link { get; set; }
        public Guid senderId { get; set; }
        public Guid? receiverId { get; set; }
        [DefaultValue(false)]
        public bool isViewed { get; set; }
        public NotifyStatus status { get; set; }
    }
}
