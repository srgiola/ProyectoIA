using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SQLFreshRotten.api.Models
{
    public class UserReview
    {
        [Key]
        public long Id                  { get; set; }
        public long User                { get; set; }
        public long Movie               { get; set; }
        [Precision(3,1)]
        public decimal ReviewScore      { get; set; }
        public string ReviewContent     { get; set; } = "";
        public string ReviewStatus      { get; set; } = "";
        public DateTime ReviewDate      { get; set; }
    }
}
