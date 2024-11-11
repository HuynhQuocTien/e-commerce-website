using e_commerce_website.Enums;

namespace e_commerce_website.Helper.Order
{
    public class StatusOrderRequest
    {
        public int orderId { get; set; }
        public OrderStatus status { get; set; }
    }
}
