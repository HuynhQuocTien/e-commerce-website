namespace e_commerce_website.Helper.Product
{
    public class ProductSearchRequest
    {
        public int? categoryId { get; set; }
        public int? providerId { get; set; }
        public string? searchKey { get; set; }
    }
}
