namespace Backend.Business.src.Utils
{
    public class Message<T>
    {
        private User sender { get; set; }
        private string message { get; set; }
        private T addon { get; set; }
    }
}