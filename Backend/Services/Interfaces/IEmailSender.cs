using e_commerce_website.Models;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendMailOrderBill(Message message, List<OrderDetailViewModel> listData, int total);

    }
}
