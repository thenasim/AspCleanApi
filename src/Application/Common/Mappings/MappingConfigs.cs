using Application.TestUsers.Responses;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mappings;

public class MappingConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestUser, TestUserResponse>()
            .Map(dest => dest.ShortIntro, src => $"Name: {src.FullName}, Gender: {src.Gender.ToString()}");
    }
}
