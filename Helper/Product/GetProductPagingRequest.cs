namespace e_commerce_website.Helper.Product
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }
        public int? categoryId { get; set; }
    }
}
