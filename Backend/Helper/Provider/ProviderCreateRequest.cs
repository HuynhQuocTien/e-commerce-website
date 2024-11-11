using e_commerce_website.Enums;

namespace e_commerce_website.Helper.Provider
{
    public class ProviderCreateRequest
    {
        public string name { get; set; }

        public ActionStatus status { get; set; }
    }
}
