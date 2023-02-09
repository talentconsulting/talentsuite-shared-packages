using System;
using TalentConsulting.TalentSuite.Shared.UI.Configuration;

namespace TalentConsulting.TalentSuite.Shared.UI
{
    public class UrlBuilder
    {
        private readonly LinkGenerator _generator;
        
        public UrlBuilder(string environment) 
        {
            _generator = new LinkGenerator(environment);
        }

        public string AccountsLink()
        {
            return AccountsLink(string.Empty);
        }

        public string AccountsLink(string routeName, params string[] args)
        {
            if (string.IsNullOrWhiteSpace(routeName))
                return _generator.AccountsLink("/");

            var route = Routes.Accounts[routeName];
            
            if (args != null && args.Length > 0)
                route = string.Format(route, args);

            return _generator.AccountsLink(route);
        }

        public string FinanceLink(string routeName, params string[] args)
        {
            var route = Routes.Finance[routeName];
            
            if (args != null && args.Length > 0)
                route = string.Format(route, args);

            return _generator.FinanceLink(route);
        }

        public string UsersLink(string routeName, params string[] args)
        {
            var route = Routes.Identity[routeName];
            
            if (args != null && args.Length > 0)
                route = string.Format(route, args);

            return _generator.UsersLink(route);
        }

        public string CommitmentsV2Link(string routeName, params string[] args)
        {
            var route = Routes.CommitmentsV2[routeName];

            if (args != null && args.Length > 0)
                route = string.Format(route, args);

            return _generator.CommitmentsV2Link(route);
        }


        public string RecruitLink(string routeName, params string[] args)
        {
            var route = Routes.Recruit[routeName];
            
            if (args != null && args.Length > 0)
                route = string.Format(route, args);

            return _generator.RecruitLink(route);
        }

        public string ActiveSection(NavigationSection section, string routeName, params string[] args)
        {
            switch (section)
            {
                case NavigationSection.TalentSuiteHome:
                    return RecruitLink("Home", args);
                case NavigationSection.TalentSuiteReports:
                    return CommitmentsV2Link("Reports", args);
                case NavigationSection.TalentSuiteTimesheets:
                default:
                    return AccountsLink(routeName, args);
            }
        }
    }
}