using System;
using Microsoft.Extensions.DependencyInjection;
using TalentConsulting.TalentSuite.Shared.UI.Configuration;

namespace TalentConsulting.TalentSuite.Shared.UI
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMaMenuConfiguration(this IServiceCollection services, string logoutRouteName, string identityClientId, string environment)
        {
            ValidateArguments(logoutRouteName, identityClientId);
            
            services.AddSingleton(new UrlBuilder(environment.ToLower()));
            
            services.AddSingleton(new PageConfiguration(identityClientId, logoutRouteName));
        }

        private static void ValidateArguments(string logoutRouteName, string identityClientId)
        {
            if (string.IsNullOrWhiteSpace(logoutRouteName))
            {
                throw new ArgumentException("Needs a valid value", nameof(logoutRouteName));
            }

            if (string.IsNullOrWhiteSpace(identityClientId))
            {
                throw new ArgumentException("Needs a valid value", nameof(identityClientId));
            }
        }
    }
}