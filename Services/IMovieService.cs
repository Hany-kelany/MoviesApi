using DevCreedApi2.Models;

namespace DevCreedApi2.Services;

public interface IMovieService
{
    Task<IEnumerable<Movie>> Getall();
    Task<Movie> GetbyId(int id);
    Task<Movie> Add(Movie movie);
    Movie Update(Movie movie);
    Movie Delete(Movie movie);
    Task<bool> Isvalidgenre(int id);
}
