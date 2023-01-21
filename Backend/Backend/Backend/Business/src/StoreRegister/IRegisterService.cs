using System;
using Backend.Business.src.Utils;

namespace Backend.Business.src.StoreRegister
{
    public interface IRegisterService
    {
        void changeEmployee(int newEmployee);
        void add_store_register(int storeID, string password);
        void add_employee_to_store_register(int storeID, string password, int employeeID);
        void login(int storeID, int employeeID);
        void logout();
        void addPurchase(int budgetNumber, string description, float cost);
        void removePurchase();
        string printPurchases();
    }
}

