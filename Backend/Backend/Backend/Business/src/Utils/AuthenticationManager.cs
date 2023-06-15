using System;
using System.Collections.Generic;
using Backend.Access;
using Backend.Business.src.StoreRegister;
using Member = Backend.Business.Utils.Member;

namespace Backend.Business.src.Utils
{
    public sealed class AuthenticationManager
    {
        
        public static Dictionary<int, string> idToEmail = new Dictionary<int, string>();
        public static Dictionary<int, string> userIdToName = new Dictionary<int, string>();
        public static Dictionary<int, string> storeIdToName = new Dictionary<int, string>();
        public static Dictionary<int, List<int>> StoreToEmployees = new Dictionary<int, List<int>>();
        
        
        private static AuthenticationManager? instance;
        private static readonly object padlock = new object();
        
        private AuthenticationManager()
        {
            idToEmail = new Dictionary<int, string>();
            StoreToEmployees = new Dictionary<int, List<int>>();


            foreach (StoreEmployee storeEmployee in DBManager.Instance.LoadStoreEmployees())
            {
                if (!StoreToEmployees.ContainsKey(storeEmployee.storeId))
                    StoreToEmployees[storeEmployee.storeId] = new List<int>();

                StoreToEmployees[storeEmployee.storeId].Add(storeEmployee.UserId);
            }
            
            /*
            foreach (Backend.Access.Member member in DBManager.Instance.LoadMembers())
            {
                userIdToName.Add(member.UserId, member.Name);
            }
            
            
            foreach (Backend.Access.Store store in DBManager.Instance.LoadStores())
            {
                storeIdToName.Add(store.storeId, store.storeName);
            }
            */


            
        }

        public string getStoreName(int id)
        {
            return storeIdToName[id];
        }
        
        public string getUserName(int id)
        {
            return userIdToName[id];
        }

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

        public void AddUser(int userId, string email, string name)
        {
            idToEmail.Add(userId, email);
            userIdToName.Add(userId, email);
        }
        
        public void AddStore(int storeId, string name)
        {
            storeIdToName.Add(storeId, name);
            
            if (!StoreToEmployees.ContainsKey(storeId))
                StoreToEmployees.Add(storeId, new List<int>());
        }
        
        public void AddStoreEmployee(int userId, int storeId)
        {
            if (!StoreToEmployees.ContainsKey(storeId))
                StoreToEmployees[storeId] = new List<int>();

            StoreToEmployees[storeId].Add(userId);
        }

        public void Login(int id, string email)
        {
            if(!idToEmail[id].Equals(email))
                throw new Exception("Email doesnt match");
        }

        /*
         * Return the store name upon success, otherwise returns null
         */
        public bool CheckWorkingPrivilege(int storeID, int employeeID)
        {
            return (StoreToEmployees[storeID].Contains(employeeID));
        }

    }
}