namespace Backend.Access;

public class Store
{
    public int storeId;
    public string storeName;
    public string photoLink;


    public Store(int storeId, string storeName, string photoLink)
    {
        this.storeId = storeId;
        this.storeName = storeName;
        this.photoLink = photoLink;
    }
}