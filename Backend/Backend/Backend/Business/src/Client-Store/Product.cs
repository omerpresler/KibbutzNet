using System.Collections.Generic;
using Backend.Business.src.Client_Store;
using System.IO;

namespace Backend.Business.src.Client_Store
{
    public class Product : Iproduct
    {
        public int productId { get; set; }
        
        public string name { get; set; }
        public string description { get; set; }

        //We represent the location of the file in our file system usg string
        public List<string> files;
        private static int _nextProdId;

        
        public Product(string name, string description, List<string> files)
        {
            productId = AssignProdId();
            this.description = description;
            this.files = files;
            this.name = name;
        }
        
        public Product(string name, string description)
        {
            productId = AssignProdId();
            this.description = description;
            files = new List<string>();
            this.name = name;
        }

        private static int AssignProdId()
        {
            return Interlocked.Increment(ref _nextProdId);
        }
        
        public string AddFile(string filePath)
        {
            if (!filePath.Contains(filePath))
            {
                files.Add(filePath);
                return "added file to the product";
            }

            return "the file is already in the post";
        }

        public string RemoveFile(string filePath)
        {
            if (files.Contains(filePath))
            {
                files.Remove(filePath);
                return "the file was uplouded correctally";
            }

            return "the file dosent exist";
        }
    }
}