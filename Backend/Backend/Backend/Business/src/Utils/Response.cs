namespace Backend.Business.src.Utils
{
    public class Response<T>
    {
        public T value { get; set; }
        public bool exceptionHasOccured { get; set; }
        public string errorMessage { get; set; }
        
        
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