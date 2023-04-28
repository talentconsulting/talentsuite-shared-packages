using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TalentConsulting.TalentSuite.ApiHelpers.Healthchecks
{
    public class ReadinessHealthCheckAsync : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                return Task.FromResult(HealthCheckResult.Healthy("Readiness OK."));
            }
            catch (Exception)
            {

                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "Readiness NOT OK."));
            }
        }
    }
}