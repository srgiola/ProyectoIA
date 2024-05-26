using Microsoft.EntityFrameworkCore;
using SQLFreshRotten.api.Models;

namespace SQLFreshRotten.api.ProviderContext
{
    public class DbCtx : DbContext
    {
        public DbSet<User> Users   { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        public DbSet<Portada> Portadas { get; set; }

        public DbCtx (DbContextOptions<DbCtx> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(GetConnectionString(), MySqlServerVersion(), options =>
                {
                    options = options.EnableRetryOnFailure();
                });
            }
        }

        private MySqlServerVersion MySqlServerVersion()
            => new MySqlServerVersion(new Version(8, 0, 4));

        /// <summary>
        /// Por tiempo, dejamos la conexion 
        /// quemada, que se levanta en un docker
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
            => "Server=localhost;Uid=root;Pwd=root;port=3306;Database=perceptron";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                
                entity.Property(property => property.Id)
                      .HasColumnName("id_user");
                entity.Property(property => property.FirstName)
                      .HasColumnName("firstname");
                entity.Property(property => property.LastName)
                      .HasColumnName("lastname");
                entity.Property(property => property.Email)
                      .HasColumnName("email");
                entity.Property(property => property.UserName)
                      .HasColumnName("username");
                entity.Property(property => property.Password)
                      .HasColumnName("password");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");

                entity.Property(property => property.Id)
                      .HasColumnName("id_movie");
                entity.Property(property => property.Title)
                      .HasColumnName("movie_title");
                entity.Property(property => property.Description)
                      .HasColumnName("movie_info");
                entity.Property(property => property.Genres)
                      .HasColumnName("genres");
                entity.Property(property => property.Directors)
                      .HasColumnName("directors");
                entity.Property(property => property.Authors)
                      .HasColumnName("authors");
                entity.Property(property => property.Actors)
                      .HasColumnName("actors");
                entity.Property(property => property.StreamingReleaseDate)
                      .HasColumnName("streaming_release_date")
                      .HasColumnType("date");
                entity.Property(property => property.Runtime)
                      .HasColumnName("runtime");
                entity.Property(property => property.ProductionCompany)
                      .HasColumnName("production_company");
                entity.Property(property => property.TomatometerStatus)
                      .HasColumnName("tomatometer_status");
                entity.Property(property => property.TomatometerRating)
                      .HasColumnName("tomatometer_rating");
                entity.Property(property => property.TomatometerCount)
                      .HasColumnName("tomatometer_count");
            });

            modelBuilder.Entity<UserReview>(entity =>
            {
                entity.ToTable("user_reviews");

                entity.Property(property => property.Id)
                      .HasColumnName("id_user_reviews");
                entity.Property(property => property.User)
                      .HasColumnName("id_users");
                entity.Property(property => property.Movie)
                      .HasColumnName("id_movie");
                entity.Property(property => property.ReviewScore)
                      .HasColumnName("review_score");
                entity.Property(property => property.ReviewContent)
                      .HasColumnName("review_content");
                entity.Property(property => property.ReviewStatus)
                      .HasColumnName("review_status");
                entity.Property(property => property.ReviewDate)
                      .HasColumnName("review_date");
            });

            modelBuilder.Entity<Portada>(entity =>
            {
                entity.ToTable("portadas");
                entity.Property(property => property.Id)
                      .HasColumnName("id_portada");
                entity.Property(property => property.Movie)
                      .HasColumnName("id_movie");
            });
        }
    }
}
