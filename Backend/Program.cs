using System;
using System.Runtime.InteropServices;
using Backend.Business.src.StoreRegister;


namespace Backend
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var service = new Business.src.StoreRegister.Service();
            service.add_store_register(1, "abc");
            service.add_employee_to_store_register(1, "abc", 1);
            while (true)
            {
                Console.WriteLine("what would you like to do?");
                Console.WriteLine("1. LOGIN");
                Console.WriteLine("2. LOGOUT");
                Console.WriteLine("3. ADD PURCHASE");
                Console.WriteLine("4. PRINT PURCHASES");
                Console.WriteLine("5. EXIT");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        service.login();
                        break;
                    case 2:
                        service.logout();
                        break;
                    case 3:
                        service.addPurchase();
                        break;
                    case 4:
                        service.printPurchases();
                        break;
                    case 5:
                        Console.WriteLine("BYE- BYE!");
                        return;
                    default:
                        Console.WriteLine("Wrong input. Please try again.");
                        break;
                }
            }
        }
    }
}