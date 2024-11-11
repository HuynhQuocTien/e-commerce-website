using e_commerce_website.Helper.Statistics;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IStatisticsService
    {
        IQueryable<RevenueStatisticsViewModel> RevenueStatistics(RevenueStatisticsRequest request);
        IQueryable<ProductViewModel> ProductStatistics(ProductStatisticsRequest request);
        List<StatusOrderStatistics> StatusOrderStatistics();
    }
}
