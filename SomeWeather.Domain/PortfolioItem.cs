namespace SomeWeather.Domain
{
    public class PortfolioItem : Entity
    {
        public string ThumbnailPath { get; set; }

        public string ImagePath { get; set; }

        public PortfolioItemType Type { get; set; }

        public string ClientName { get; set; }

        public string Url { get; set; }

        public bool IsLive { get; set; }

        public string Description { get; set; }
    }

    public enum PortfolioItemType
    {
        Design = 0,
        Programming = 1,
        DesignAndProgramming = 2
    }
}