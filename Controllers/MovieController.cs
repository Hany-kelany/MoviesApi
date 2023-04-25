using DevCreedApi2.DTO;
using DevCreedApi2.Models;
using DevCreedApi2.Services;
using Microsoft.AspNetCore.Mvc;
namespace DevCreedApi2.Controllers;

[Route("Api/[Controller]")]
[ApiController]

public class MovieController : Controller
{
    private readonly List<string> _allowdextensions = new List<string> { ".png", ".jpg" };
    private readonly long _maxsize = 1048576;

    private readonly IMovieService _movieService;
    private readonly IGenreService _genreService;

    public MovieController(IMovieService movieService, IGenreService genreService)
    {
        _movieService = movieService;
        _genreService = genreService;
    }



    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        var Movie = await _movieService.Getall();

        return Ok(Movie);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetbyId(int id)
    {
        var Movie = await _movieService.GetbyId(id);
        if (Movie == null)
            return BadRequest("Movie not found ");


        return Ok(Movie);
    }
    [HttpGet("Getmoviebyid")]
    public async Task<IActionResult> Getmoviebyid(int id)
    {

        var Movie = await _movieService.GetbyId(id);

        return Ok(Movie);
    }


    [HttpPost]
    public async Task<IActionResult> AddmovieAsync([FromForm] MovieDto Moviedto)
    {
        if (!_allowdextensions.Contains(Path.GetExtension(Moviedto.Poster.FileName).ToLower()))
            return BadRequest("this ex not allowd");

        if (Moviedto.Poster.Length > _maxsize)
            return BadRequest("the file size is large");

        var isvalidgenre = await _movieService.Isvalidgenre(Moviedto.GenreId);
        if (!isvalidgenre)
            return NotFound("Id of genre not found");

        using var datastream = new MemoryStream();
        await Moviedto.Poster.CopyToAsync(datastream);
        var Movie = new Movie
        {
            Title = Moviedto.Title,
            Year = Moviedto.Year,
            Poster = datastream.ToArray(),
            Rate = Moviedto.Rate,
            StoryLine = Moviedto.StoryLine,
            GenreId = Moviedto.GenreId
        };

        _movieService.Add(Movie);
        return Ok(Movie);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> EditAsync(int id, [FromForm] MovieDto Dto)
    {
        var Movie = await _movieService.GetbyId(id);

        if (Movie == null)
            return NotFound("not found Movie");


        var isvalidgenre = await _movieService.Isvalidgenre(Dto.GenreId);
        if (!isvalidgenre)
            return NotFound("Id of genre not found");

        if (Dto.Poster != null)
        {
            if (!_allowdextensions.Contains(Path.GetExtension(Dto.Poster.FileName).ToLower()))
                return BadRequest("this ex not allowd");

            if (Dto.Poster.Length > _maxsize)
                return BadRequest("the file size is large");

            using var datastream = new MemoryStream();
            await Dto.Poster.CopyToAsync(datastream);

            Movie.Poster = datastream.ToArray();
        }


        Movie.Title = Dto.Title;
        Movie.Year = Dto.Year;
        Movie.StoryLine = Dto.StoryLine;
        Movie.Rate = Dto.Rate;
        Movie.GenreId = Dto.GenreId;

        _movieService.Update(Movie);
        return Ok(Movie);

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletAsync(int id)
    {
        var Movie = await _movieService.GetbyId(id);
        if (Movie == null)
            return NotFound("Not found Movie ");

        _movieService.Delete(Movie);
        return Ok(Movie);
    }


}
