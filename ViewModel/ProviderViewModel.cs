using e_commerce_website.Enums;
using Microsoft.Build.Framework;
using System.ComponentModel;

namespace e_commerce_website.ViewModel
{
    public class ProviderViewModel
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [DefaultValue(ActionStatus.Display)]
        public ActionStatus status { get; set; }
    }
}
