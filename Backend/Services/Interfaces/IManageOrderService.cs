using e_commerce_website.Enums;
using e_commerce_website.Helper.Order;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IManageOrderService
    {
        Task<List<OrderViewModel>> GetAllOrderNotConfirm();
        Task<List<OrderViewModel>> SearchOrder(SearchOrderRequest request);
        Task<List<OrderDetailViewModel>> GetOrderDetailByOrderId(int orderId);
        Task<ResultOrderViewModel> confirmShippingAndSendMailBillOrder(StatusOrderRequest request);
        Task<OrderViewModel> GetOrderByOrderId(int orderId);
        Task<bool> CancelOrder(CancelOrderRequest request);
        Task<bool> confirmSuccessOrder(StatusOrderRequest request);
        //
        Task<List<OrderViewModel>> GetAllOrderSuccess();
        Task<List<OrderViewModel>> GetAllOrderDelivering();
        Task<List<OrderViewModel>> GetAllOrderCancelled();
        Task<bool> SetStatusNotConfirm(int orderId, OrderStatus status);
    }
}
