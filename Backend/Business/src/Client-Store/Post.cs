using System;
using System.Collections.Generic;
using System.IO;

namespace Backend.Business.src.Client_Store
{
    public class Post : Ipost
    {
        public int Postid { get; set; }

        public string header { get; set; }

        public List<string> FileList { get; set; }

        public Post(int postid, String header)
        {
            Postid = postid;
            this.header = header;
        }

        public string addFile(string filePath)
        {
            if (!filePath.Contains(filePath))
            {
                FileList.Add(filePath);
                return "added file to the post";
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