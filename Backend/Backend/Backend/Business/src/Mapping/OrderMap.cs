using System;
using Backend.Business.Client_Member;
using FluentNHibernate.Mapping;

namespace Backend.Business.src.Mapping
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("order");
            Id(m => m.orderID).GeneratedBy.Identity();
        }
    }
}