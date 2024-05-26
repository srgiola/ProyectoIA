using NLog;
using SQLFreshRotten.api.Abstracts;
using SQLFreshRotten.api.LogicProcess.implements;
using SQLFreshRotten.api.LogicProcess.managmentfiles;
using SQLFreshRotten.api.Models;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.LogicProcess.db
{
    public class DbUserReview
    {
        private readonly DbCtx _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public DbUserReview(DbCtx context)
        {
            _context = context;
        }

        public async Task SetDefaultReviews()
        {
            List<UserReview> defaultMovies = DefaultUsersReview();

            LoadFactory factory = new EfLoadFactory(_context);
            var addRange = factory.AddItemRange<UserReview>();
            await addRange.InsertRange(defaultMovies);
        }

        private List<UserReview> DefaultUsersReview()
        {
            List<UserReview> usersReview = new();

            try
            {
                ManagmentFile managmentFile = new();
                List<string> contentInRows = managmentFile.GetContentFile("reviews.txt");
                contentInRows.ForEach(row =>
                {
                    string[] reviewInformation = row.Split(',');

                    UserReview newUserReview = new UserReview
                    {
                        User = Convert.ToInt64(reviewInformation[0]),
                        Movie = Convert.ToInt64(reviewInformation[1]),
                        ReviewScore = GetPrecision(Convert.ToDecimal(reviewInformation[2])),
                        ReviewContent = reviewInformation[3],
                        ReviewStatus = reviewInformation[4],
                        ReviewDate = Convert.ToDateTime(reviewInformation[5]),
                    };
                    usersReview.Add(newUserReview);
                });
            }
            catch (Exception ex)
            {
                _logger.Error("Problemas al obtener los review de los usuarios default");
                _logger.Error($"Ms: {ex.Message}, St: {ex.StackTrace}");
            }

            return usersReview;
        }

        private decimal GetPrecision(decimal value)
            => Math.Round(value, 1);
    }
}
