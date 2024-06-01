using SQLFreshRotten.api.Abstracts;
using SQLFreshRotten.api.LogicProcess.implements;
using SQLFreshRotten.api.Models;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.LogicProcess.db
{
    public class DbPortada
    {
        private readonly DbCtx _context;

        public DbPortada (DbCtx context)
        {
            _context = context;
        }

        public async Task SetDefault ()
        {
            List<Portada> portadas = _context.Movies
                                         .Select(property => new Portada
                                         {
                                              Id = 0,
                                              Movie = property.Id
                                         })
                                         .ToList();

            LoadFactory factory = new EfLoadFactory(_context);
            var addRange = factory.AddItemRange<Portada>();
            await addRange.InsertRange(portadas);
        }
    }
}
