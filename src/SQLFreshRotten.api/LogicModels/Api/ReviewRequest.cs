namespace SQLFreshRotten.api.LogicModels.Api
{
    public class ReviewRequest
    {
        public long Movie       { get; set; }
        public string User        { get; set; }
        public string Critc     { get; set; } = "";
        public decimal Score    { get; set; }
    }
}
