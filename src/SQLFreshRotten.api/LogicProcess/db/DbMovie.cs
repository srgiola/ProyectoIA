using NLog;
using SQLFreshRotten.api.Abstracts;
using SQLFreshRotten.api.LogicProcess.implements;
using SQLFreshRotten.api.LogicProcess.managmentfiles;
using SQLFreshRotten.api.Models;
using SQLFreshRotten.api.ProviderContext;
using System.Text.RegularExpressions;

namespace SQLFreshRotten.api.LogicProcess.db
{
    public class DbMovie
    {
        private readonly DbCtx _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public DbMovie (DbCtx context)
        {
            _context = context;
        }

        public async Task SetDefaultMovies ()
        {
            List<Movie> defaultMovies = DefaultMovies();
            List<string> movieInSystem = _context.Movies
                                                 .Select(property => property.Title)
                                                 .ToList();

            List<Movie> moviesAdd = defaultMovies.Where(propery
                                                    => !movieInSystem.Contains(propery.Title)
                                                 )
                                                 .ToList();

            LoadFactory factory = new EfLoadFactory(_context);
            var addRange = factory.AddItemRange<Movie>();
            await addRange.InsertRange(moviesAdd);
        }

        private List<Movie> DefaultMovies ()
        {
            List<Movie> movies = new();
            
            try
            {
                ManagmentFile managmentFile = new();
                List<string> contentInRows = managmentFile.GetContentFile("movies.txt");
                contentInRows.ForEach(row =>
                {
                    string[] movieInformation = row.Split('|');

                    Movie newMovie = new()
                    {
                        Title                = GetInfoCast(movieInformation[0]),
                        Description          = GetInfoCast(movieInformation[1]),
                        Genres               = GetInfoCast(movieInformation[2]),
                        Directors            = GetInfoCast(movieInformation[3]),
                        Authors              = GetInfoCast(movieInformation[4]),
                        Actors               = GetInfoCast(movieInformation[5]),
                        StreamingReleaseDate = Convert.ToDateTime(movieInformation[6]),
                        Runtime              = Convert.ToInt32(movieInformation[7]),
                        ProductionCompany    = GetInfoCast(movieInformation[8]),
                        TomatometerStatus    = GetInfoCast(movieInformation[9]),
                        TomatometerRating    = GetPrecision(Convert.ToDecimal(movieInformation[10])),
                        TomatometerCount     = Convert.ToInt32(movieInformation[11])
                    };
                    movies.Add(newMovie);
                });
            }
            catch (Exception ex)
            {
                _logger.Error("Problemas al obtener los usuarios default");
                _logger.Error($"Ms: {ex.Message}, St: {ex.StackTrace}");
            }

            return movies;
        }

        private string GetInfoCast (string info)
        {
            Match match = Regex.Match(info, "[\\S].*[\\w]");
            if (match.Success)
                return match.Value;
            else
                throw new Exception($"El regex esta fallando. Cadena = | {info} |");
        }

        private decimal GetPrecision(decimal value)
            => Math.Round(value, 1);

    }
}
