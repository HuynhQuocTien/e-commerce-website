using e_commerce_website.Models;

namespace e_commerce_website.ViewModel
{
    public class OrderDetailViewModel
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public int unitPrice { get; set; }
        public int sale { get; set; }
        //foreign key
        public int productId { get; set; }
        public List<Product> product { get; set; }
        //foreign key
        public int orderId { get; set; }
        public Order order { get; set; }
    }
}
