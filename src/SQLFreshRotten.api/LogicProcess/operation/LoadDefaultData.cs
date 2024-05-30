using SQLFreshRotten.api.LogicProcess.db;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.LogicProcess.operation
{
    public class LoadDefaultData
    {
        private readonly DbCtx _context;

        public LoadDefaultData (DbCtx context)
        {
            _context = context;
        }

        public async Task Load ()
        {
            DbMovie dbMovie = new(_context);
            await dbMovie.SetDefaultMovies();
            
            DbUser dbUser = new(_context);
            await dbUser.SetDafaultUsers();

            DbUserReview dbUserReview = new(_context);
            await dbUserReview.SetDefaultReviews();

            DbPortada dbPortada = new(_context);
            await dbPortada.SetDefault();
        }
    }
}
