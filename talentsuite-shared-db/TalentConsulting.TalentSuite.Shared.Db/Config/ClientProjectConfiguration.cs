using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentConsulting.TalentSuite.Shared.Db.Entities;

namespace TalentConsulting.TalentSuite.Shared.Db.Config;

public class ClientProjectConfiguration
{
    public void Configure(EntityTypeBuilder<ClientProject> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();
        builder.Property(t => t.ClientId)
            .IsRequired();
        builder.Property(t => t.ProjectId)
            .IsRequired();

        builder.Property(t => t.Created)
            .IsRequired();

    }
}
