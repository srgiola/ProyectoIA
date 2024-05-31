using Microsoft.AspNetCore.Mvc;
using SQLFreshRotten.api.LogicModels.Api;
using SQLFreshRotten.api.LogicProcess.db;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly DbCtx _context;
        private readonly IHostEnvironment _environment;

        public MovieController(DbCtx context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet("all-movies/")]
        public async Task<ActionResult<ResponseRequest<dynamic>>> AllMovies()
        {
            try
            {
                DbMovie dbMovie = new (_context, _environment.ContentRootPath);
                
                return Ok(new ResponseRequest<dynamic>()
                {
                    Data = new { Movies = await dbMovie.GetMovieInformation() },
                    Description = "Peliculas, obtenidas",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseRequest<dynamic>()
                {
                    Data = new { Movies = new List<MovieInformation>() },
                    Description = "Peliculas, no obtenidas",
                    FailReponse = new FailResponse()
                    {
                        IsFail = true,
                        Exception = ex.Message
                    }
                });
            }
        }
    }
}
