using System;
using Microsoft.Extensions.DependencyInjection;
using TalentConsulting.TalentSuite.Shared.UI.Configuration;

namespace TalentConsulting.TalentSuite.Shared.UI
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMaMenuConfiguration(this IServiceCollection services, string logoutRouteName, string environment)
        {
            ValidateArguments(logoutRouteName);
            
            services.AddSingleton(new UrlBuilder(environment.ToLower()));
            
            services.AddSingleton(new PageConfiguration(logoutRouteName));
        }

        private static void ValidateArguments(string logoutRouteName)
        {
            if (string.IsNullOrWhiteSpace(logoutRouteName))
            {
                throw new ArgumentException("Needs a valid value", nameof(logoutRouteName));
            }

        }
    }
}