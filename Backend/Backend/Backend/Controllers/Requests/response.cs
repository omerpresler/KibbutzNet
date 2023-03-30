namespace Backend.Controllers.Requests
{
    public class response<T>
    {
        private T Value;
        private bool wasExecption;

        public response(T value,bool wasExecption)
        {
            Value = value;
            this.wasExecption = wasExecption;
        }

        public T getValue()
        {
            return Value;
        }

        public bool getWasExecption()
        {
            return wasExecption;
        }

    }

}
