using Microsoft.AspNetCore.Mvc;
using SQLFreshRotten.api.LogicModels.Api;
using SQLFreshRotten.api.LogicProcess.db;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly DbCtx _context;

        public UserController (DbCtx context) 
        {
            _context = context;
        }

        [HttpGet("validate-user/")]
        public ActionResult<ResponseRequest<dynamic>>  ValidateUser([FromQuery] string user, [FromQuery] string password)
        {
            try
            {
                DbUser dbUser = new(_context);
                bool hasAcces = dbUser.AuthenticateUser(user, password);
                if (!hasAcces)
                    throw new Exception("Contraseña o Usuario, incorrecto");

                return Ok(new ResponseRequest<dynamic>()
                {
                    Data = new { HasAccess = hasAcces },
                    Description = "Usuario, authenticado",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseRequest<dynamic>()
                {
                    Data = new { HasAccess = false },
                    Description = "Usuario, no authenticado",
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
