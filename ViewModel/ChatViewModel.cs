using e_commerce_website.Models;

namespace e_commerce_website.ViewModel
{
    public class ChatViewModel
    {
        public int id { get; set; }
        public DateTime createDate { get; set; }
        public string content { get; set; }
        public Guid senderId { get; set; }
        public AppUser sender { get; set; }
        public Guid receiverId { get; set; }
        public AppUser receiver { get; set; }
    }
}
