namespace SQLFreshRotten.api.LogicModels.Api
{
    public class MovieInformation
    {
        public long Id { get; set; }
        public string Title       { get; set; } = "";
        public string Description { get; set; } = "";
        public string MovieDevURL    { get; set; } = "";
        public string MovieProdURL { get; set; } = "";
    }
}
