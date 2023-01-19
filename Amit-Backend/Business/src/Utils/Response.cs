namespace Backend.Business.src.Utils
{
    public class Response<T>
    {
        private T value { get; set; }
        private bool exceptionHasOccured { get; set; }
        private string errorMessage { get; set; }
        
        
        public Response(T value)
        {
            this.value = value;
            this.exceptionHasOccured = false;
            this.errorMessage = "";
        }
        
        public Response(bool exceptionHasOccured, string errorMessage)
        {
            this.exceptionHasOccured = exceptionHasOccured;
            this.errorMessage = errorMessage;
        }
    }
}