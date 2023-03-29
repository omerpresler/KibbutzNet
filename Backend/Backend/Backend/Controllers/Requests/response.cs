namespace Backend.Controllers.Requests
{
    public class response<T>
    {
        public T value { get; set; }
        public bool wasExecption { get; set; }

        public response(T value,bool wasExecption)
        {
            this.value = value;
            this.wasExecption = wasExecption;
        }

        public T getValue()
        {
            return value;
        }

        public bool getWasExecption()
        {
            return wasExecption;
        }

    }

}
