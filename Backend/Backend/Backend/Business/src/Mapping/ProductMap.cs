using Backend.Business.src.Client_Store;
using FluentNHibernate.Mapping;

namespace Backend.Business.src.Mapping;

public class ProductMap : ClassMap<Product>
{
    public ProductMap()
    {
        Table("product");
        Id(m => m.productId).GeneratedBy.Identity();
        Map(m => m.name);
        Map(m => m.description);
    }
}