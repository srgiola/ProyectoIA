using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SQLFreshRotten.api.Models
{
    public class Movie
    {
        [Key]
        public long Id                          { get; set; }
        public string Title                     { get; set; } = "";
        public string Description               { get; set; } = "";
        public string Genres                    { get; set; } = "";
        public string Directors                 { get; set; } = "";
        public string Authors                   { get; set; } = "";
        public string Actors                    { get; set; } = "";
        public DateTime StreamingReleaseDate    { get; set; }
        public int Runtime                      { get; set; }
        public string ProductionCompany         { get; set; } = "";
        public string TomatometerStatus         { get; set; } = "";
        [Precision(3, 1)]
        public decimal TomatometerRating        { get; set; }
        public int TomatometerCount             { get; set; }
    }
}
