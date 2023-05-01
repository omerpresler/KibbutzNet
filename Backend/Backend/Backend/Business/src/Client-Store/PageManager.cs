using System.Collections.Generic;
using Backend.Business.src.Utils;
using Backend.Business.Utils;

namespace Backend.Business.src.Client_Store
{
    public class PageManager
    {
        public virtual int storeID { set; get; }
        public string StoreName;
        private List<Post> posts;
        private List<Product> products;

   
        public PageManager(int storeId, string storeName)
        {
            storeID = storeId;
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

        public Response<Product> AddProduct(Product product)
        {
            if (products.Find(x => x.productId == product.productId) != null)
                return new Response<Product>(true, "Product with such id already exist");
                
            products.Add(product);
            return new Response<Product>(product);
        }
        
        public Response<Product> RemoveProduct(int productId)
        {
            Product? prod = products.Find(x => x.productId == productId);
            
            if(prod == null)
                return new Response<Product>(true, "Product with such id already exist");

            products.Remove(prod);
            return new Response<Product>(prod);
        }
    }
}