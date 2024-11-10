using e_commerce_website.Enums;

namespace e_commerce_website.Helper.Order
{
    public class CancelOrderRequest
    {
        public int orderId { get; set; }
        public OrderStatus status { get; set; }
        public OrderStatus statusRollBack { get; set; }
        public string note { get; set; }
    }
}
