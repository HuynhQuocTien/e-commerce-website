using MimeKit;

namespace e_commerce_website.Models
{
    public class Message
    {
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => MailboxAddress.Parse(x)));
            Subject = subject;
            Content = content;
        }
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
