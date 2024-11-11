namespace e_commerce_website.Helper.Chat
{
    public class ChatCreateRequest
    {
        public string connectionId { get; set; }

        public string content { get; set; }
        public Guid senderId { get; set; }
        public Guid? receiverId { get; set; }
    }
}
