using e_commerce_website.Enums;

namespace e_commerce_website.Helper.Order
{
    public class SearchOrderRequest
    {
        public string keyWord { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public OrderStatus status { get; set; }
    }
}
