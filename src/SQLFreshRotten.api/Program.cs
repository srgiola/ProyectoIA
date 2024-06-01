
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
            builder.Services.AddDbContextPool<DbCtx>(options =>
            {
                string connection = "Server=db;Uid=root;Pwd=root;port=3306;Database=perceptron";
                //string connection = "Server=localhost;Uid=root;Pwd=root;port=3306;Database=perceptron";
                MySqlServerVersion serverVersion = new(new Version(8,0,4));
                options.UseMySql(connection, serverVersion);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
        
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SQLFreshRotten API 1.0.0");
            });

            // INFO: Es para estudiar no debeira de tener cors
            app.UseRouting();
            app.UseCors("AllowAll");

            app.MapGet("/", context 
                                => Task.Run(() => context.Response.Redirect("/swagger/index.html"))
                       );
            app.UseAuthorization();
            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
