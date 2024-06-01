using Microsoft.EntityFrameworkCore;
using NLog;
using SQLFreshRotten.api.Abstracts;
using SQLFreshRotten.api.LogicModels.Api;
using SQLFreshRotten.api.LogicProcess.implements;
using SQLFreshRotten.api.LogicProcess.managmentfiles;
using SQLFreshRotten.api.LogicProcess.microservices;
using SQLFreshRotten.api.Models;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.LogicProcess.db
{
    public class DbUserReview
    {
        private readonly DbCtx _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private string _response = string.Empty;

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

        public async Task<bool> AddReview (ReviewRequest parameters)
        {
            bool isAdded;
            try
            {
                CriticRequest critic = new()
                {
                    critic = parameters.Critc
                };

                ReviewService service = new();
                ResponseRewiew criticType = await service.GetReview(critic);
                if (criticType == null || string.IsNullOrEmpty(criticType.result))
                    throw new Exception("Critica no obtenida");

                User? userFind = await _context.Users.Where(p => p.UserName == parameters.User)
                                                    .FirstOrDefaultAsync();

                if (userFind == null)
                    throw new Exception("Usuario no encontrado");

                UserReview userReview = new()
                {
                      Movie = parameters.Movie,
                      ReviewContent = parameters.Critc,
                      ReviewDate = DateTime.Now,
                      ReviewScore = GetPrecision(parameters.Score),
                      ReviewStatus = criticType.result,
                      User = userFind.Id
                };

                LoadFactory factory = new EfLoadFactory(_context);
                var addRange = factory.AddItemRange<UserReview>();
                await addRange.InsertOne(userReview);

                isAdded = true;
                _response = criticType.result;
            }
            catch (Exception ex)
            {
                _logger.Error("No se agrego la critica del usuario");
                _logger.Error($"Ms: {ex.Message}, St: {ex.StackTrace}");
                isAdded = false;
            }

            return isAdded;
        }
    
        public async Task<List<UserCritic>> GetReviews (long movieId)
        {
            List<UserCritic> critics = await (
                                                from user_review in _context.UserReviews
                                                join user in _context.Users
                                                    on user_review.User equals user.Id
                                                where user_review.Movie == movieId
                                                select new UserCritic
                                                {
                                                    Critic = user_review.ReviewContent,
                                                    User = user.Id,
                                                    UserName = user.UserName,
                                                    ResultIa = user_review.ReviewStatus
                                                }
                                             ).ToListAsync();

            return critics;
        }

        public string GetCriticResult()
            => _response;
    }
}
