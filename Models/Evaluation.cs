using e_commerce_website.Enums;
using System.ComponentModel;

namespace e_commerce_website.Models
{
    public class Evaluation
    {
        public Evaluation()
        {
            Replies = new List<Reply>();
        }
        public int id { get; set; }
        public int rating { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        [DefaultValue(EvaluationStatus.Confirm)]
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
