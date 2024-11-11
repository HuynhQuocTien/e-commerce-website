namespace e_commerce_website.Helper.Chat
{
    public class ChatGetMessageRequest
    {
        public Guid senderId { get; set; }
        public Guid? receiverId { get; set; }
    }
}
