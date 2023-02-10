namespace TalentConsulting.TalentSuite.Shared.UI
{
    internal class LinkGenerator
    {
        private readonly string _environment;
        private readonly string _domainPart;

        public LinkGenerator(string environment)
        {
            _environment = "talent";
            _domainPart = "dev";
        }
        public string HomeLink(string route)
        {
            return $"https://home.{_environment}.{_domainPart}.gov.uk{route}";
        }

        public string ReportsLink(string route)
        {
            return $"https://reports.{_environment}.{_domainPart}.gov.uk{route}";
        }
    }
}