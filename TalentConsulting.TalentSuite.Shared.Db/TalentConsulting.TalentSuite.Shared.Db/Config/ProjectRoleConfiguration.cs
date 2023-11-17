using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentConsulting.TalentSuite.Shared.Db.Entities;

namespace TalentConsulting.TalentSuite.Shared.Db.Config;

public class ProjectRoleConfiguration
{
    public void Configure(EntityTypeBuilder<ProjectRole> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();
        builder.Property(t => t.Name)
            .IsRequired();
        builder.Property(t => t.Technical)
            .IsRequired();
        builder.Property(t => t.Description)
            .IsRequired();

        builder.Property(t => t.Created)
            .IsRequired();
    }
}
