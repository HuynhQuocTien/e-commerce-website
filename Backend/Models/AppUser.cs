using Microsoft.AspNetCore.Identity;
using e_commerce_website.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using e_commerce_website.Enums;

namespace e_commerce_website.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            Orders = new List<Order>();
        }
        [Required]
        public string displayname { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public string? avatar { get; set; }
        public bool gender { get; set; }
        public DateTime birthDay { get; set; }

        [DefaultValue(ActionStatus.Display)]
        public ActionStatus status { get; set; }
        //relative n - 1
        public virtual ICollection<Order> Orders { get; set; }
    }
}
