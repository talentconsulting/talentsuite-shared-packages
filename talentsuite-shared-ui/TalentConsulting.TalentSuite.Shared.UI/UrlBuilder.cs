namespace TalentConsulting.TalentSuite.Shared.UI
{
    public class UrlBuilder
    {
        private readonly LinkGenerator _generator;
        
        public UrlBuilder(string environment) 
        {
            _generator = new LinkGenerator(environment);
        }

        public string HomeLink()
        {
            return HomeLink();
        }

        public string ReportsLink()
        {
            return ReportsLink();
        }

    }
}