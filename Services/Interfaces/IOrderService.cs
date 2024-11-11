using e_commerce_website.Helper.Order;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> Create(OrderCreateRequest request);
        Task<List<OrderViewModel>> GetOrderListByUserId(Guid userId);
    }
}
