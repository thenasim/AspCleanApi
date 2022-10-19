using Api.Contracts.Products;
using Api.Contracts.TestUsers;
using Domain.Entities;
using Mapster;

namespace Api.Common.Mappings;

public class MappingConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductResponse>();
        config.NewConfig<TestUser, TestUserResponse>();
    }
}
