using Api.Contracts.Products;
using Domain.Entities;
using Mapster;

namespace Api.Common.Mappings;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductResponse>();
    }
}
