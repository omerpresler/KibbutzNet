using Backend.Business.Utils;
using FluentNHibernate.Mapping;

namespace Backend.Business.src.Mapping;

public class MemberMap : ClassMap<Member>
{
    MemberMap()
    {
        Table("member");
        //Id( m => m.)
        //Map(m => )
    }
}