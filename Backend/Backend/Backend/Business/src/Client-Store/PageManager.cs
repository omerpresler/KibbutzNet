using System.Collections.Generic;
using Backend.Business.src.Utils;
using Backend.Business.Utils;

namespace Backend.Business.src.Client_Store
{
    public class PageManager
    {
        public string StoreName { get; set; }
        public List<Post> posts { get; set; }
        public List<Product> products { get; set; }

        public PageManager(string storeName)
        {
            this.StoreName = storeName;
            this.posts = new List<Post>();
            this.products = new List<Product>();
        }

        public Response<Post> AddPost(Post post)
        {
            if (posts.Find(x => x.Postid == post.Postid) != null)
                return new Response<Post>(true, "Post with the same id already exist");
            posts.Add(post);
            return new Response<Post>(post);
        }

        public Response<Post> RemovePost(int postId)
        {
            Post? postToRemove = posts.Find(x => x.Postid == postId);
            
            if(postToRemove == null)
                return new Response<Post>(true, "There is no such post");

            posts.Remove(postToRemove);
            return new Response<Post>(postToRemove);
        }

        public string AddProduct(Product product)
        {
            products.Add(product);
            return "added the post";
        }
        
        public string RemoveProduct(int ProductIdToRemove)
        {
            foreach (var postToRemove in posts)
            {
                if (ProductIdToRemove == postToRemove.Postid)
                    posts.Remove(postToRemove);
                return "removed sucssfully";
            }

            return "didnt find the post to remove";
        }
        
    }
}