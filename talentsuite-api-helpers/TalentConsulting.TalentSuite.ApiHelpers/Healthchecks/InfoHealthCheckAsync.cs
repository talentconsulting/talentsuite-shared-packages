using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Dynamic;
using System.Reflection;

namespace TalentConsulting.TalentSuite.ApiHelpers.Healthchecks
{
    public class InfoHealthCheckAsync : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // get assembly version
                Assembly assembly = Assembly.GetEntryAssembly();
                AssemblyInformationalVersionAttribute versionAttribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
                string assemblyVersion = versionAttribute.InformationalVersion;

                // create the dynamic object
                dynamic result = new ExpandoObject();
                result.version = assemblyVersion;

                return Task.FromResult(HealthCheckResult.Healthy("assembly: " + assembly));
            }
            catch (Exception)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "Info NOT OKs"));
            }
        }
    }
}
