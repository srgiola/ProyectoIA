using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLFreshRotten.api.LogicModels.Api;
using SQLFreshRotten.api.LogicProcess.db;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.Controllers
{
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

                return BadRequest(new ResponseRequest<dynamic>()
                {
                    Data = new { HasAccess = dbUser.AuthenticateUser(user, password) },
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
