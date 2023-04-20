using Backend.Business.src.Client_Store;
using FluentNHibernate.Mapping;

namespace Backend.Business.src.Mapping;

public class PostMap : ClassMap<Post>
{
    public PostMap()
    {
        Table("post");
        Id(m => m.Postid, "postID").GeneratedBy.Identity();
    }
}