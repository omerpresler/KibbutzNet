using Backend.Business.src.Client_Store;
using FluentNHibernate.Mapping;

namespace Backend.Business.src.Mapping;

public class StoreMap : ClassMap<PageManager>
{
    public StoreMap()
    {
        Table("store");
        Id(m => m.storeID).GeneratedBy.Identity();
    }
    
}