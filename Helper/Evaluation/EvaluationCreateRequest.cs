namespace e_commerce_website.Helper.Evaluation
{
    public class EvaluationCreateRequest
    {
        public int rating { get; set; }
        public string title { get; set; }
        public string content { get; set; }

        //foreign key
        public int productId { get; set; }

        //foreign key
        public Guid userId { get; set; }
    }
}
