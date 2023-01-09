namespace Backend.Business.src.Utils
{
    public abstract class User
    {
        public int userId { get; set; }
        private int storeId;
        private string name;
        private string phoneNumber;

        protected User(int userId, int storeId, string name, string phoneNumber)
        {
            this.userId = userId;
            this.storeId = storeId;
            this.name = name;
            this.phoneNumber = phoneNumber;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || typeof(object) != this.GetType())
                return false;
            return this.userId == ((User)obj).userId;
        }
    }
}