using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLFreshRotten.api.LogicModels.Api;
using SQLFreshRotten.api.LogicProcess.db;
using SQLFreshRotten.api.LogicProcess.microservices;
using SQLFreshRotten.api.Models;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly DbCtx _context;

        public ReviewController (DbCtx context)
        {
            _context = context;
        }
        
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

        [HttpPost("create-review")]
        public async Task<ActionResult<ResponseRequest<dynamic>>> CreateReview ([FromBody] ReviewRequest parameters)
        {
            try
            {
                DbUserReview dbUserReview = new(_context);
                bool isAdded = await dbUserReview.AddReview(parameters);
                if (!isAdded)
                    throw new Exception("No se ha creado el modelo");

                return Ok(new ResponseRequest<dynamic>()
                {
                    Data = new { IsAdded = true, CriticResult = dbUserReview.GetCriticResult() },
                    Description = "Critica, agregada",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseRequest<dynamic>()
                {
                    Data = new { IsAdded = false, CriticResult = string.Empty },
                    Description = "Critica, no agregada",
                    FailReponse = new FailResponse()
                    {
                         Exception = ex.Message,
                         IsFail = true
                    }
                });
            }
        }

        [HttpGet("user-reviews/")]
        public async Task<ActionResult<ResponseRequest<List<UserCritic>>>> UserReviews ([FromQuery] long movieId)
        {
            try
            {
                DbUserReview dbUserReview = new(_context);
                
                return Ok(new ResponseRequest<dynamic>()
                {
                    Data = await dbUserReview.GetReviews(movieId),
                    Description = "Criticas, obtenidas",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseRequest<dynamic>()
                {
                    Data = new List<UserCritic>(),
                    Description = "Criticas, no obtenidas",
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
