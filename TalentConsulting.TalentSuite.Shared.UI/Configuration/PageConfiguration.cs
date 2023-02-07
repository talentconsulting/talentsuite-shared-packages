using System.Collections.Generic;

namespace TalentConsulting.TalentSuite.Shared.UI.Configuration
{
    public class PageConfiguration
    {
        public PageConfiguration(string accountsOidcClientId, string localLogoutRouteName)
        {
            LocalLogoutRouteName = localLogoutRouteName;
            AccountsOidcClientId = accountsOidcClientId;
        }
        public string AccountsOidcClientId { get; }
        public string LocalLogoutRouteName { get; }
    }
}