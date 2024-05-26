using Newtonsoft.Json;
using NLog;
using RestSharp;
using SQLFreshRotten.api.LogicModels.Api;

namespace SQLFreshRotten.api.LogicProcess.microservices
{
    public class ReviewService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task<ResponseRewiew> GetReview (CriticRequest parameters)
        {
            ResponseRewiew result = new();

            try
            {
                const string url_service = "http://ia-clasific:5000/";
                const string route_endpoint = "/classify";
                var options = new RestClientOptions(url_service);

                var client = new RestClient(options);
                var request = new RestRequest(route_endpoint, Method.Post);
                request.AddHeader("Content-Type", "application/json");

                string body = JsonConvert.SerializeObject(parameters);

                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = await client.ExecuteAsync(request);

                string content = response.Content ?? "";
                _logger.Info($"Content = {response.Content}, Status = {response.StatusCode},  Message = {response.ErrorMessage}");

                result = JsonConvert.DeserializeObject<ResponseRewiew>(content) ?? throw new Exception("Contenido, no deserealilzado");
            }
            catch (Exception ex)
            {
                _logger.Error("Problemas con el servicio");
                _logger.Error($"Ms: {ex.Message}, St: {ex.StackTrace}");
            }

            return result;
        }
    }
}
