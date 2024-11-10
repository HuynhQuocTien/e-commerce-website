using e_commerce_website.Enums;

namespace e_commerce_website.Helper.Provider
{
    public class ProviderUpdateRequest
    {
        public int id { get; set; }
        public string name { get; set; }

        public ActionStatus status { get; set; }
    }
}
