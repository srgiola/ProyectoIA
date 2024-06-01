using System.ComponentModel.DataAnnotations;

namespace SQLFreshRotten.api.Models
{
    public class Portada
    {
        [Key]
        public long Id { get; set; }
        public long Movie { get; set; }
    }
}
