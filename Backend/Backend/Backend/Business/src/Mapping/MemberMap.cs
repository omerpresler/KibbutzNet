using Backend.Business.Utils;
using FluentNHibernate.Mapping;

namespace Backend.Business.src.Mapping;

public class MemberMap : ClassMap<Member>
{
    public MemberMap()
    {
        Table("member");
        Id(m => m.UserId).GeneratedBy.Identity();
        Map(m => m.CurrHouse, "houseID");
        Map(m => m.accountNum, "accountNumber");
    }
}