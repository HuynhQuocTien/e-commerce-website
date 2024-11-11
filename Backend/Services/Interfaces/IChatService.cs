using e_commerce_website.Helper.Chat;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IChatService
    {
        Task<int> CreateMessage(ChatCreateRequest request);
        Task<ChatViewModel> GetMessageById(int chatId);
        Task<List<ChatViewModel>> GetMessages(ChatGetMessageRequest request);
        Task<List<UserViewModel>> GetUserOnlines(List<Guid> userIds);
    }
}
