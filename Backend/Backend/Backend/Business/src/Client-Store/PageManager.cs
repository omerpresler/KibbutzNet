using System.Collections.Generic;
using System.Text.Json;
using Backend.Access;
using Backend.Business.src.Utils;
using Backend.Business.Utils;

namespace Backend.Business.src.Client_Store
{
    public class PageManager
    {
        public int storeId;
        private List<Post> posts;
        private List<Product> products;
        private static int _nextId;

   
        public PageManager(int storeId)
        {
            this.storeId = storeId;
            this.posts = new List<Post>();
            this.products = new List<Product>();
            
            try
            {
                _nextId = DBManager.Instance.getMaxPostId();
            }
            catch (Exception e)
            {
                _nextId = 0;
            }

            foreach (Access.Post post in DBManager.Instance.LoadPostsPerStore(storeId))
            {
                posts.Add(new Post(post.postId, post.header, post.photoLink));
            }
        }
        
        private static int AssignId()
        {
            return Interlocked.Increment(ref _nextId);
        }

        public Response<Post> AddPost(string header, string photoLink)
        {
            Post post = new Post(AssignId(), header, photoLink);
            posts.Add(post);
            DBManager.Instance.AddPost(post.postId, storeId, header, photoLink);
            return new Response<Post>(post);
        }

        public Response<Post> RemovePost(int postId)
        {
            Post? postToRemove = posts.Find(x => x.postId == postId);
            
            if(postToRemove == null)
                return new Response<Post>(true, "There is no such post");

            posts.Remove(postToRemove);
            DBManager.Instance.RemovePost(postToRemove.postId);
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
        
        public Response<List<Post>> getPosts()
        {
            return new Response<List<Post>>(posts);
        }
    }
}