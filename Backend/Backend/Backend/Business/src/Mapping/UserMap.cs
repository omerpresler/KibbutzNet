using Backend.Business.Utils;
using FluentNHibernate.Mapping;

namespace Backend.Business.src.Mapping;

public class UserMap : ClassMap<User>
{
    public UserMap()
    {
        Table("user");
        Id(m => m.UserId, "userID").GeneratedBy.Identity();
        Map(m => m.Name);
        Map(m => m.PhoneNumber, "phone");
    }
}