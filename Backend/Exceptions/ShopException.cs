namespace e_commerce_website.Exceptions
{
    public class ShopException : Exception
    {
        public ShopException()
        {

        }
        public ShopException(string message) : base(message)
        {

        }
        public ShopException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
