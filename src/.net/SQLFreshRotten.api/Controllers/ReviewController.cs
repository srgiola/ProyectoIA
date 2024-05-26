using Microsoft.AspNetCore.Mvc;
using SQLFreshRotten.api.LogicModels.Api;
using SQLFreshRotten.api.LogicProcess.db;
using SQLFreshRotten.api.LogicProcess.microservices;
using SQLFreshRotten.api.Models;

namespace SQLFreshRotten.api.Controllers
{
    public class ReviewController : Controller
    {
        [HttpPost("review-type/")]
        public async Task<ActionResult<ResponseRequest<ResponseRewiew>>> ReviewType([FromBody] CriticRequest paramerts)
        {
            try
            {
                ReviewService service = new();
                ResponseRewiew criticType = await service.GetReview(paramerts);
                return Ok(new ResponseRequest<dynamic>()
                {
                    Data = new { Result = criticType },
                    Description = "Tipo de review, obtenida",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseRequest<dynamic>()
                {
                    Data = new ResponseRewiew(),
                    Description = "Tipo de review, no obtenida",
                    FailReponse = new FailResponse()
                    {
                        Exception = ex.Message,
                        IsFail = true
                    }
                });
            }
        }
    }
}
