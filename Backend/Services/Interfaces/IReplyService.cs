using e_commerce_website.Helper.Reply;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IReplyService
    {
        Task<int> Create(ReplyCreateRequest request);
        Task<ReplyViewModel> getReplyById(int replyId);

    }
}
