namespace Backend.Business.src.Utils
{
    public class Member : User
    {
        private int budgetNumber { get; set; }

        public Member(int userId, int storeId, string name, string phoneNumber, int budgetNumber) : base(userId, storeId, name, phoneNumber)
        {
            this.budgetNumber = budgetNumber;
        }

    }
}