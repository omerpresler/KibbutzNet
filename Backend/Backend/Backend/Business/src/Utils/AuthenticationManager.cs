using System;
using System.Collections.Generic;
using Backend.Business.src.StoreRegister;
using Backend.Business.Utils;

namespace Backend.Business.src.Utils
{
    public sealed class AuthenticationManager
    {
        private AuthenticationManager()
        {
            store_registers = new Dictionary<KeyValuePair<int, string>, int>();
            stores = new Dictionary<int, string>();
            _storeRegisters = new List<StoreRegister.StoreRegister>();
            idToEmail = new Dictionary<int, string>();
        }

        private Dictionary<KeyValuePair<int, string>, int> store_registers;
        public Dictionary<int, string> stores;
        private List<Business.src.StoreRegister.StoreRegister> _storeRegisters;

        public static Dictionary<int, string> idToEmail;
        private static AuthenticationManager? instance;
        private static readonly object padlock = new object();

        public static AuthenticationManager Instance {
            get {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AuthenticationManager();
                    }
                    return instance;
                }
            }
        }

        public void AddUser(int userId, string email)
        {
            idToEmail.Add(userId, email);
        }
        
        public void AddStore(int storeId, string storeName)
        {
            stores.Add(storeId, storeName);
        }


        public Response<int> logIn(string name, string email, int Id)
        {
            return new Response<int>(-1);
        }

        public void add_store_register(int storeID, string password)
        {
            stores.Add(storeID, password);
            _storeRegisters.Add(new StoreRegister.StoreRegister(storeID));
        }

        public void add_employee_to_store_register(int storeID, string password, int employeeID)
        {
            if (stores.TryGetValue(storeID, out password))
            {
                var store = new KeyValuePair<int, string>(storeID, password);
                store_registers.Add(store, employeeID);
            }
        }
        
        public bool login_register(int storeID, int employeeID, string password)
        {
            var store = new KeyValuePair<int, string>(storeID, password);
            return store_registers.TryGetValue(store, out employeeID);
        }

        public Member Login(int id, string email)
        {
            if(idToEmail[id].Equals(email))
                return new Member(id, "PlaceHolder", "PlaceHolder", email);

            throw new Exception("Email doesnt match");
        }

        /*
         * Return the store name upon success, otherwise returns null
         */
        public String CheckWorkingPrivilege(int storeID, int employeeID)
        {
            //TODO: confirm that this employee work at this store
            return stores[storeID];
        }

    }
}