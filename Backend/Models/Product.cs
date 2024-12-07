using e_commerce_website.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Color = e_commerce_website.Enums.Color;
using Size = e_commerce_website.Enums.Size;

namespace e_commerce_website.Models
{
    public class Product
    {
        public Product()
        {
            Evaluations = new List<Evaluation>();
            Images = new List<Image>();
        }
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int importPrice { get; set; }
        public int price { get; set; }
        [DefaultValue(0)]
        public int sale { get; set; }
        public string? description { get; set; }
        [DefaultValue(5)]
        public int? rating { get; set; }

        [DefaultValue(ActionStatus.Display)]
        public ActionStatus status { get; set; }
        public Size? size { get; set; }
        public Color? color { get; set; }
        [DefaultValue(1)]
        public int amount { get; set; }
        [DefaultValue(0)]
        public int viewCount { get; set; }

        //foreign key
        public int? categoryId { get; set; }
        public Category category { get; set; }
        //image
        public virtual ICollection<Image> Images { get; set; }
        //
        public int? providerId { get; set; }
        public Provider provider { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }

    }
}
