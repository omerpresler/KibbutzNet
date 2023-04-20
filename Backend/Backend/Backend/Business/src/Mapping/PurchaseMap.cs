using Backend.Business.Client_Member;
using FluentNHibernate.Mapping;

namespace Backend.Business.src.Mapping;

public class PurchaseMap : ClassMap<Purchase>
{
    public PurchaseMap()
    {
        Table("purchase");
        Id(m => m.purchaseID).GeneratedBy.Identity();
    }
}