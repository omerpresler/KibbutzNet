using System;
using System.Collections.Generic;
using Backend.Business.src.StoreRegister;

namespace Backend.Business.src.Utils
{
    public sealed class AuthenticationManager
    {
        // The Singleton's constructor should always be private to prevent
        // direct construction calls with the `new` operator.
        private AuthenticationManager() { }

        // The Singleton's instance is stored in a static field. There there are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static AuthenticationManager _instance = null;

        // This is the static method that controls the access to the singleton
        // instance. On the first run, it creates a singleton object and places
        // it into the static field. On subsequent runs, it returns the client
        // existing object stored in the static field.

        private Dictionary<KeyValuePair<int, string>, int> store_registers = new Dictionary<KeyValuePair<int, string>, int>();
        private Dictionary<int, string> stores = new Dictionary<int, string>();
        private List<Business.src.StoreRegister.StoreRegister> _storeRegisters = new List<StoreRegister.StoreRegister>();

        public static AuthenticationManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AuthenticationManager();
            }
            return _instance;
        }

        public void add_store_register(int storeID, string password)
        {
            stores.Add(storeID, password);
        }

        public void add_employee_to_store_register(int storeID, string password, int employeeID)
        {
            if (stores.TryGetValue(storeID, out password))
            {
                var store = new KeyValuePair<int, string>(storeID, password);
                store_registers.Add(store, employeeID);
                _storeRegisters.Add(new StoreRegister.StoreRegister(storeID, employeeID));
            }
        }
        
        public bool login_register(int storeID, int employeeID, string password)
        {
            var store = new KeyValuePair<int, string>(storeID, password);
            return store_registers.TryGetValue(store, out employeeID);
        }
        
    }
}