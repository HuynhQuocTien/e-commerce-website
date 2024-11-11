using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_website.Models
{
    [Tags("AppRole")]
    public class AppRole : IdentityRole<Guid>
    {

        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
