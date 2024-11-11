using e_commerce_website.Enums;
using System.ComponentModel;

namespace e_commerce_website.ViewModel
{
    public class ImageViewModel
    {
        public int id { get; set; }
        public string urlImage { get; set; }
        [DefaultValue(ActionStatus.Display)]
        public ActionStatus status { get; set; }
        public int? productId { get; set; }
    }
}
