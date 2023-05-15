using System;
using System.Collections.Generic;
using System.IO;

namespace Backend.Business.src.Client_Store
{
    public class Post : Ipost
    {
        public int postId { get; set; }

        public string header { get; set; }

        public string photoLink { get; set; }

        public Post(int postId, String header, string photoLink)
        {
            this.postId = postId;
            this.header = header;
            this.photoLink = photoLink;
        }
    }
}