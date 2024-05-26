namespace SQLFreshRotten.api.LogicModels.Api
{
    public class FailResponse
    {
        public bool IsFail      { get; set; } = false;
        public string Exception { get; set; } = "";
    }
}
