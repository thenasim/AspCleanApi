using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TestUserConfiguration : IEntityTypeConfiguration<TestUser>
{
    public void Configure(EntityTypeBuilder<TestUser> builder)
    {
        // Configure
    }
}
