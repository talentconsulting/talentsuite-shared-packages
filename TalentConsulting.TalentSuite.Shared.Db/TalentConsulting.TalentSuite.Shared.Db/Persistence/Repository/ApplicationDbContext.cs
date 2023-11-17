﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TalentConsulting.TalentSuite.Shared.Db.Entities;
using TalentConsulting.TalentSuite.Shared.Db.Interceptors;

namespace TalentConsulting.TalentSuite.Shared.Db.Persistence.Repository;

public interface IApplicationDbContext
{
    DbSet<Client> Clients { get; }
    DbSet<ClientProject> ClientProjects { get; }
    DbSet<Contact> Contacts { get; }
    DbSet<Project> Projects { get; }
    DbSet<ProjectRole> ProjectRoles { get; }
    DbSet<Report> Reports { get; }
    DbSet<Risk> Risks { get; }
    DbSet<Sow> Sows { get; }
    DbSet<SowFile> SowFiles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}


public class ApplicationDbContext : DbContext, IApplicationDbContext
{
#if USE_DISPATCHER
    private readonly IDomainEventDispatcher _dispatcher;
#endif
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext
        (
            DbContextOptions<ApplicationDbContext> options,
#if USE_DISPATCHER
            IDomainEventDispatcher dispatcher,
#endif
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
        //IConfiguration configuration
        )
        : base(options)
    {
#if USE_DISPATCHER
        _dispatcher = dispatcher;
#endif
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

#if USE_DISPATCHER
        // ignore events if no dispatcher provided
        var entitiesWithEvents = ChangeTracker
            .Entries()
            .Select(e => e.Entity as EntityBase<string>)
            .Where(e => e?.DomainEvents != null && e.DomainEvents.Any())
            .ToArray();


#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        if (entitiesWithEvents != null && entitiesWithEvents.Any())
        
            var entitiesWithEventsGuids = new List<EntityBase<Guid>>();
            foreach (var entityWithEvents in entitiesWithEvents)
            {
                var t = entityWithEvents?.ToString();
                entitiesWithEventsGuids.Add(Guid.Parse(t);
            }

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
        }

#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#endif
        return result;
    }


    public DbSet<Client> Clients => Set<Client>();
    public DbSet<ClientProject> ClientProjects => Set<ClientProject>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectRole> ProjectRoles => Set<ProjectRole>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<Risk> Risks => Set<Risk>();
    public DbSet<Sow> Sows => Set<Sow>();
    public DbSet<SowFile> SowFiles => Set<SowFile>();

}

