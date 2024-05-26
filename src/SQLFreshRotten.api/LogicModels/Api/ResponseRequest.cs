namespace SQLFreshRotten.api.LogicModels.Api
{
    public class ResponseRequest<T> where T : class
    {
        public T Data { get; set; }
        public string Description { get; set; } = "";
        public FailResponse FailReponse { get; set; } = new (); 
    }
}
