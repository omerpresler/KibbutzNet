using System;
using Backend.Business.src.Utils;

namespace Backend.Business.src.StoreRegister
{
    public interface IRegisterService
    {
        void add_store_register(int storeID, string password);
        void add_employee_to_store_register(int storeID, string password, int employeeID);
        void login();
        void logout();
        void addPurchase();
        void removePurchase();
    }
}

