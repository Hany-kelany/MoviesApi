using DevCreedApi2.Models;
using System.ComponentModel.DataAnnotations;
namespace DevCreedApi2.DTO;

public class MovieDto
{
    public string? Title { get; set; }
    public int Year { get; set; }
    public Double Rate { get; set; }
    public IFormFile? Poster { get; set; }

    [MaxLength(2500)]
    public string? StoryLine { get; set; }

    public int GenreId { get; set; }
}
