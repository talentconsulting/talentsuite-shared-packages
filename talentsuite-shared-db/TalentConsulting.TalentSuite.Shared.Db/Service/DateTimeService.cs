using TalentConsulting.TalentSuite.Shared.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Shared.Db.Service;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
