namespace e_commerce_website.Helper.Reply
{
    public class ReplyCreateRequest
    {
        public string content { get; set; }



        //foreign key
        public Guid userId { get; set; }

        //foreign key
        public int evaluationId { get; set; }
    }
}
