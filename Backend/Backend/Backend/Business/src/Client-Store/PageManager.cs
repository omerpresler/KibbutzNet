using System.Collections.Generic;
using Backend.Business.Utils;

namespace Backend.Business.src.Client_Store
{
    public class PageManager
    {
        private string StoreName { get; set; }
        private List<Post> posts { get; set; }
        private List<Product> products { get; set; }

        public PageManager(string storeName)
        {
            this.StoreName = storeName;
            this.posts = new List<Post>();
            this.products = new List<Product>();
        }

        public string addPost(Post post)
        {
            posts.Add(post);
            return "added the post";
        }

        public string RemovePost(int postIdToRemove)
        {
            foreach (var postToRemove in posts)
            {
                if (postIdToRemove == postToRemove.Postid)
                    posts.Remove(postToRemove);
                return "removed succsufully";
            }

            return "didnt find the post to remove";
        }

        public string addProduct(Product product)
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