using System;
using System.Collections;
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
        Response<int> addPurchase(int budgetNumber, string description, float cost);
        void removePurchase();
        ArrayList printPurchases();
        ArrayList printPurchases(DateTime start);
        ArrayList printPurchases(DateTime start, DateTime end);
    }
}

