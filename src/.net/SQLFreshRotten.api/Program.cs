
using Microsoft.EntityFrameworkCore;
using SQLFreshRotten.api.LogicProcess.db;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContextPool<DbCtx>(options =>
            {
                string connection = "Server=db;Uid=root;Pwd=root;port=3306;Database=perceptron";
                MySqlServerVersion serverVersion = new(new Version(8,0,4));
                options.UseMySql(connection, serverVersion);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
        
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
