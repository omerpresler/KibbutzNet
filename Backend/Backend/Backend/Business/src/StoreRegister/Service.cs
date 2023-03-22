using System;
using Backend.Business.src.Utils;

namespace Backend.Business.src.StoreRegister
{
    public class RegisterService : IRegisterService
    {
        private AuthenticationManager auth;
        private bool loged_in { set; get; }
        private int employee { set; get; }
        private StoreRegister? register { set; get; }
        public RegisterService()
        {
            auth = AuthenticationManager.GetInstance();
            loged_in = false;
            register = null;
            employee = -1;
        }

        public void changeEmployee(int newEmployee)
        {
            employee = newEmployee;
        }

        public void AddStoreRegister(int storeID, string password)
        {
            auth.add_store_register(storeID, password);
        }

        public void AddEmployeeToStoreRegister(int storeID, string password, int employeeID)
        {
            auth.add_employee_to_store_register(storeID, password, employeeID);
        }

        public void login(int storeID, int employeeID)
        {
            //TODO: Add passwords?
            string password = "Ignore Password for now";
            //if (auth.login_register(storeID, employeeID, password))
            if(true)
            {
                loged_in = true;
                employee = employeeID;
                register = new StoreRegister(storeID);
            }
        }

        public void logout()
        {
            if (loged_in)
            {
                loged_in = false;
                employee = -1;
                Console.WriteLine("Logout succeed!");
                return;
            }

            Console.WriteLine("Logout failed! you are already logged in...");
        }
        
        public bool addPurchase(int budgetNumber, string description, float cost)
        {
            if (loged_in && register != null)
            {
                return register.addPurchase(budgetNumber, description, cost, employee);
            }

            return false;
        }
        
        public void removePurchase()
        {
            if (loged_in && register != null)
            {
                Console.WriteLine("Please enter the purchase number:");
                var purchase = Convert.ToInt32(Console.ReadLine());
                if (register.removePurchase(purchase))
                {
                    Console.WriteLine("The purchase was removed successfully!");
                    return;
                }

                Console.WriteLine("The removal failed!");
                return;
            }
            Console.WriteLine("You are logged out. log in first...");
        }
        
        public string printPurchases()
        {
            return loged_in && register != null ? register.printPurchases() : "No employee logged in";
        }
        
        public string printPurchases(DateTime start)
        {
            if (loged_in && register != null)
            {
                return register.printPurchases(start);
            }

            return "No employee logged in";
        }
        
        public string printPurchases(DateTime start, DateTime end)
        {
            if ( loged_in & register != null)
            {
                return register.printPurchases(start, end);
            }

            return "No employee logged in";
        }
    }
    

}