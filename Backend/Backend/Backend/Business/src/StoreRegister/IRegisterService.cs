using System;
using Backend.Business.src.Utils;

namespace Backend.Business.src.StoreRegister
{
    public interface IRegisterService
    {
        void changeEmployee(int newEmployee);
        void AddStoreRegister(int storeID, string password);
        void AddEmployeeToStoreRegister(int storeID, string password, int employeeID);
        void login(int storeID, int employeeID);
        void logout();
        bool addPurchase(int budgetNumber, string description, float cost);
        void removePurchase();
        string printPurchases();
        string printPurchases(DateTime start);
        string printPurchases(DateTime start, DateTime end);
    }
}

