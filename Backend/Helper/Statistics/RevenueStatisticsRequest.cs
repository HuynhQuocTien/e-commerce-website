using e_commerce_website.Enums;

namespace e_commerce_website.Helper.Statistics
{
    public class RevenueStatisticsRequest
    {
        public DayOrMonth? option { get; set; }
        public int? month { get; set; }
        public int? year { get; set; }
    }
}
