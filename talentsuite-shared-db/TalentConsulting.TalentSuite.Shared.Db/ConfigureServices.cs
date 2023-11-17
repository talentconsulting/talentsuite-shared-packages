﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal;
using System.Text;
using TalentConsulting.TalentSuite.Shared.Common;
using TalentConsulting.TalentSuite.Shared.Common.Interfaces;
using TalentConsulting.TalentSuite.Shared.Db.Interceptors;
using TalentConsulting.TalentSuite.Shared.Db.Persistence.Repository;
using TalentConsulting.TalentSuite.Shared.Db.Service;

namespace TalentConsulting.TalentSuite.Shared.Db;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        var useDbType = configuration.GetValue<string>("UseDbType");

        switch (useDbType)
        {
            case "UseInMemoryDatabase":
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TalentDb"));
                break;

            case "UseSqlServerDatabase":
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? String.Empty));
                break;

            case "UseSqlLite":
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection") ?? String.Empty));
                break;

            case "UsePostgresDatabase":
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection") ?? String.Empty)
                    .ReplaceService<ISqlGenerationHelper, NpgsqlSqlGenerationLowercasingHelper>());
                break;

            default:
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TalentDb"));
                break;
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }


#pragma warning disable EF1001

    /// <summary>A replacement for <see cref="NpgsqlSqlGenerationHelper"/>
    /// to convert PascalCaseCsharpyIdentifiers to alllowercasenames.
    /// So table and column names with no embedded punctuation
    /// get generated with no quotes or delimiters.</summary>
    public class NpgsqlSqlGenerationLowercasingHelper : NpgsqlSqlGenerationHelper
    {
        //Don't lowercase ef's migration table
        const string dontAlter = "__EFMigrationsHistory";
        static string Customize(string input) => input == dontAlter ? input : input.ToLower();
        public NpgsqlSqlGenerationLowercasingHelper(RelationalSqlGenerationHelperDependencies dependencies)
            : base(dependencies) { }
        public override string DelimitIdentifier(string identifier)
            => base.DelimitIdentifier(Customize(identifier));
        public override void DelimitIdentifier(StringBuilder builder, string identifier)
            => base.DelimitIdentifier(builder, Customize(identifier));
    }
#pragma warning restore EF1001
}
