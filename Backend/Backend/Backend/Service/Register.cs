﻿using System.Collections;
using Backend.Business.src.Utils;

namespace Backend.Service;
using Backend.Business.src.StoreRegister;

public class Register
{
    private static Dictionary<int, IRegisterService> registers;
    
    private static Register instance;
    private static readonly object padlock = new object();

    private Register()
    {
        registers = new Dictionary<int, IRegisterService>();
        this.OpenRegister(0,0);

    }

    public static Register Instance {
        get {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Register();
                }
                return instance;
            }
        }
    }


    public bool OpenRegister(int StoreId, int EmployeeId)
    {
        if (!registers.ContainsKey(StoreId))
        {
            IRegisterService register = new RegisterService();
            register.login(StoreId, EmployeeId);
            registers.Add(StoreId, register);
        }
        else
        {
            IRegisterService register = registers[StoreId];
            register.changeEmployee(EmployeeId);
        }

        Console.WriteLine(registers.Count);
        return true;
    }
    
    public Response<int> addPurchase(int StoreId, int BudgetNumber, string Description, float Cost)
    {
        return registers.ContainsKey(StoreId) ? registers[StoreId].addPurchase(BudgetNumber, Description, Cost) : new Response<int>(true, "The register has not been opened");
    }
    
    public ArrayList SeePurchaseHistory(int StoreId)
    {
        IRegisterService register = registers[StoreId];
            
        return register.printPurchases();;
    }
    //register -add new purchse see purchse history
    //store-client-get report
    
    public ArrayList SeePurchaseHistory(int StoreId, DateTime start)
    {
        IRegisterService register = registers[StoreId];
            
        return register.printPurchases(start);;
    }
    
    public ArrayList SeePurchaseHistory(int StoreId, DateTime start, DateTime end)
    {
        IRegisterService register = registers[StoreId];
            
        return register.printPurchases(start, end);;
    }
}