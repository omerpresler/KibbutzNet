namespace Backend.Business.src.Utils
{
    public class Response<T>
    {
        private T value { get; set; }
        private bool exceptionHasOccured { get; set; }
        private string errorMessage { get; set; }

    }
}