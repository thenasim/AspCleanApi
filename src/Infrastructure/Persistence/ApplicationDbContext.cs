﻿using System.Reflection;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Infrastructure;
// using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;

    [Obsolete]
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
    {
        //_mediator = this.GetService<IMediator>();
        _mediator = mediator;

        NpgsqlConnection.GlobalTypeMapper.MapEnum<Gender>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Set postgres enums
        modelBuilder.HasPostgresEnum<Gender>();

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<TestUser> TestUsers => Set<TestUser>();
}
