using Backend.Business.Utils;

namespace Backend.Business.src.Mapping;
using FluentNHibernate.Mapping;

public class HouseMap : ClassMap<House>
{
    public HouseMap()
    {
        Table("house");
        Id(m => m.HouseId).GeneratedBy.Identity();
        Map(m => m.Direction);
    }
    
}