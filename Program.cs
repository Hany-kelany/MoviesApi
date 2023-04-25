using DevCreedApi2.Data;
using DevCreedApi2.Services;
using Microsoft.EntityFrameworkCore;

namespace DevCreedApi2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddCors();

        builder.Services.AddTransient<IGenreService, GenreService>();
        builder.Services.AddTransient<IMovieService, MovieService>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        //app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}