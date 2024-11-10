using e_commerce_website.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace e_commerce_website.Models
{
    public class Provider
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [DefaultValue(ActionStatus.Display)]
        public ActionStatus status { get; set; }
    }
}
