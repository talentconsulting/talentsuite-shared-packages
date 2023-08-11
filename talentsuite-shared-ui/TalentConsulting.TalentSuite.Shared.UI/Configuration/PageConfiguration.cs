using System.Collections.Generic;

namespace TalentConsulting.TalentSuite.Shared.UI.Configuration
{
    public class PageConfiguration
    {
        public PageConfiguration(string localLogoutRouteName)
        {
            LocalLogoutRouteName = localLogoutRouteName;
        }
        public string LocalLogoutRouteName { get; }
    }
}