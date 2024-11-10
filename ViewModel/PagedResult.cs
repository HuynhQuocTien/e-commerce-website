namespace e_commerce_website.ViewModel
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecord { get; set; }
    }
}
