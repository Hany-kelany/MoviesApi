using DevCreedApi2.Data;
using DevCreedApi2.DTO;
using DevCreedApi2.Models;
using Microsoft.EntityFrameworkCore;

namespace DevCreedApi2.Services;

public class MovieService : IMovieService
{
    private readonly ApplicationDbContext _context;

    public MovieService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Movie> Add(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        _context.SaveChanges();
        return movie;
    }

    public Movie Delete(Movie movie)
    {
        _context.Remove(movie);
        _context.SaveChanges();
        return movie;
    }

    public async Task<IEnumerable<Movie>> Getall()
    {
        return await _context.Movies.Include(m => m.Genre).ToListAsync();
    }

    public Task<Movie> GetbyId(int id)
    {
        var Movie = _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
        return Movie;
    }

    public Task<bool> Isvalidgenre(int id)
    {
        return _context.Genres.AnyAsync(g => g.Id == id);
    }

    public Movie Update(Movie movie)
    {

        _context.Update(movie);
        _context.SaveChanges();
        return movie;
    }
}
