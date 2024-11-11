using e_commerce_website.Enums;
using System.ComponentModel;

namespace e_commerce_website.Helper.Image
{
    public class ImageRequest
    {
        public string urlImage { get; set; }
        [DefaultValue(ActionStatus.Display)]
        public ActionStatus status { get; set; }
    }
}
