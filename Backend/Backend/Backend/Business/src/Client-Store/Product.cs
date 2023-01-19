using System.Collections.Generic;
using Backend.Business.src.Client_Store;
using System.IO;

namespace Backend.Business.src.Client_Store
{
    public class Product : Iproduct
    {
        public int Productid { get; set; }

        public string descpition { get; set; }

        //string is an addrass in the server for where the files is saved 
        public List<string> FileList { get; set; }
        
        public string addfile(string filePath)
        {
            if (!filePath.Contains(filePath))
            {
                FileList.Add(filePath);
                return "added file to the product";
            }

            return "the file is already in the post";
        }

        public string RemoveFile(string filePath)
        {
            if (FileList.Contains(filePath))
            {
                FileList.Remove(filePath);
                return "the file was uplouded correctally";
            }

            return "the file dosent exist";
        }
    }
}