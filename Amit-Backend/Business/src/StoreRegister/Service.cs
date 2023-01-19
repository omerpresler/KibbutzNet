using System;
using Backend.Business.src.Utils;

namespace Backend.Business.src.StoreRegister
{
    public class Service : IRegisterService
    {
        private AuthenticationManager auth;
        private bool loged_in { set; get; }
        private int employee { set; get; }
        private StoreRegister register { set; get; }
        public Service()
        {
            auth = AuthenticationManager.GetInstance();
            loged_in = false;
            register = null;
            employee = -1;
            Console.WriteLine("Welcome to store register!");
        }

        public void add_store_register(int storeID, string password)
        {
            auth.add_store_register(storeID, password);
        }

        public void add_employee_to_store_register(int storeID, string password, int employeeID)
        {
            auth.add_employee_to_store_register(storeID, password, employeeID);
        }

        public void login()
        {
            Console.WriteLine("Please enter the store ID:");
            var storeID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter your ID:");
            var employeeID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter password:");
            var password = Console.ReadLine();
            if (auth.login_register(storeID, employeeID, password))
            {
                loged_in = true;
                employee = employeeID;
                Console.WriteLine("Login succeed!");
                if (register == null)
                {
                    register = new StoreRegister(storeID);
                }
                return;
            }

            Console.WriteLine("Login failed");
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
        
        public void addPurchase()
        {
            if (loged_in)
            {
                Console.WriteLine("Please enter budget number:");
                var budget = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter description:");
                var description = Console.ReadLine();
                Console.WriteLine("Please enter the cost:");
                var cost = float.Parse(Console.ReadLine());
                if (register.addPurchase(budget, description, cost, employee))
                {
                    Console.WriteLine("The purchase was made successfully!");
                    return;
                }

                Console.WriteLine("The purchase failed!");
                return;
            }
            Console.WriteLine("You are logged out. log in first...");
        }
        
        public void removePurchase()
        {
            if (loged_in)
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
        
        public void printPurchases()
        {
            if (loged_in)
            {
                register.printPurchases();
                return;
            }
            Console.WriteLine("You are logged out. log in first...");
        }
    }
    

}