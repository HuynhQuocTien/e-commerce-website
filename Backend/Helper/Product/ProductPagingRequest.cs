namespace e_commerce_website.Helper.Product
{
    public class ProductPagingRequest
    {
        public SearchProductRequest search { get; set; }
        public int pageCurrent { get; set; }
        public int pageSize { get; set; }
    }
}
