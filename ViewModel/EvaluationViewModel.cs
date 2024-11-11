using e_commerce_website.Enums;
using e_commerce_website.Models;

namespace e_commerce_website.ViewModel
{
    public class EvaluationViewModel
    {
        public int id { get; set; }
        public int rating { get; set; }
        public string title { get; set; }
        public string content { get; set; }

        public EvaluationStatus status { get; set; }
        public DateTime createDate { get; set; }
        //foreign key
        public int productId { get; set; }
        public Product product { get; set; }
        //foreign key
        public Guid userId { get; set; }
        public AppUser user { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
