using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLFreshRotten.api.LogicModels.Api;
using SQLFreshRotten.api.LogicProcess.operation;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private DbCtx _context;
        public DataController (DbCtx context)
        {
            _context = context;
        }

        [HttpPost("apply-migration")]
        public ActionResult<ResponseRequest<dynamic>> ApplyMigration ()
        {
            try
            {
                _context.Database.Migrate();
                return Ok(new ResponseRequest<dynamic>()
                {
                    Data = new { IsOk = true },
                    Description = "Migracion, aplicada",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseRequest<dynamic>()
                {
                    Data = new { IsOk = false },
                    Description = "Migracion, no aplicada",
                    FailReponse = new FailResponse()
                    {
                        IsFail = true,
                        Exception = ex.Message
                    }
                });
            }
        }

        [HttpPost("load-data")]
        public async Task<ActionResult<ResponseRequest<dynamic>>> LoadData ()
        {
            try
            {
                LoadDefaultData load = new(_context);
                await load.Load();

                return Ok(new ResponseRequest<dynamic>()
                {
                    Data = new { IsLoad = false },
                    Description = "La data, ha sido agregada"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseRequest<dynamic>()
                {
                    Data = new { IsLoad = false },
                    Description = "La data, no se cargo en la base de datos",
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
