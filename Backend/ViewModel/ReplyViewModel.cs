using e_commerce_website.Enums;
using e_commerce_website.Models;

namespace e_commerce_website.ViewModel
{
    public class ReplyViewModel
    {
        public int id { get; set; }
        public string content { get; set; }

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
