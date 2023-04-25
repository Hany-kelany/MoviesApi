using DevCreedApi2.Models;

namespace DevCreedApi2.Services;

public interface IGenreService
{
    Task<IEnumerable<Genre>> Getall();
    Genre GetbyId(int id);
    Task<Genre> Add(Genre genre);
    Genre Update(Genre genre);
    Genre Delete(Genre genre);
}
