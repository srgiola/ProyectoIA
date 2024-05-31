namespace SQLFreshRotten.api.LogicModels.Api
{
    public class MovieInformation
    {
        public long Id { get; set; }
        public string Title       { get; set; } = "";
        public string Description { get; set; } = "";
        public string MovieImageB64 { get; set; } = "";
    }
}
