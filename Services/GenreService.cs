using DevCreedApi2.Data;
using DevCreedApi2.Models;
using Microsoft.EntityFrameworkCore;

namespace DevCreedApi2.Services;

public class GenreService : IGenreService
{
    private readonly ApplicationDbContext _context;

    public GenreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Genre> Add(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
        _context.SaveChanges();
        return genre;
    }

    public Genre Delete(Genre genre)
    {
        _context.Remove(genre);
        _context.SaveChanges();
        return genre;
    }

    public async Task<IEnumerable<Genre>> Getall()
    {
        return await _context.Genres.ToListAsync();
    }

    public Genre GetbyId(int id)
    {
        return _context.Genres.Find(id);

    }

    public Genre Update(Genre genre)
    {
        _context.Genres.Find(genre.Id);
        _context.Update(genre);
        _context.SaveChanges();
        return genre;

    }
}
