namespace Controllers.Requests
{
    public class RegisterInfoRequest
    {
        public int EmployeeId { get; set; }
        public int StoreId { get; set; }
        
        public RegisterInfoRequest(int EmployeeId, int StoreId)
        {
            this.EmployeeId = EmployeeId;
            this.StoreId = StoreId;
        }
    }
}