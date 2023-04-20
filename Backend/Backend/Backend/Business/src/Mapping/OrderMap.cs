using System;
using Backend.Business.src.Client_Store;
using FluentNHibernate.Mapping;

namespace Backend.Business.src.Mapping
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("order");
            Id(m => m.orderID).GeneratedBy.Identity();
            Map(m => m.memberId);
            Map(m => m.cost);
            Map(m => m.date);
            Map(m => m.status);
            Map(m => m.description);
        }
    }
}