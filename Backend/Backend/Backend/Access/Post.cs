namespace Backend.Access;

public class Post
{
    public int postId { get; set; }
    public int storeId { get; set; }

    public string header { get; set; }

    public string photoLink { get; set; }

    public Post(int postId, int storeId, String header, string photoLink)
    {
        this.postId = postId;
        this.storeId = storeId;
        this.header = header;
        this.photoLink = photoLink;
    }
}