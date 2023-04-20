using Backend.Business.Utils;
using FluentNHibernate.Mapping;

namespace Backend.Business.src.Mapping;

public class UserMap : ClassMap<User>
{
    public UserMap()
    {
        Table("user");
        Id(m => m.userId).GeneratedBy.Identity();
        Map(m => m.name);
        Map(m => m.phoneNumber);
    }
}