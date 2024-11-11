using e_commerce_website.Enums;
using System.ComponentModel;

namespace e_commerce_website.Models
{
    public class Reply
    {
        public int id { get; set; }
        public string content { get; set; }
        [DefaultValue(ActionStatus.Display)]
        public ActionStatus status { get; set; }
        public DateTime createDate { get; set; }
        //foreign key
        public Guid userId { get; set; }
        public AppUser user { get; set; }
        //foreign key
        public int evaluationId { get; set; }
        public Evaluation evaluation { get; set; }
    }
}
