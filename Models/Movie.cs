using System.ComponentModel.DataAnnotations;

namespace DevCreedApi2.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Year { get; set; }
    public Double Rate { get; set; }
    public byte[]? Poster { get; set; }

    [MaxLength(2500)]
    public string? StoryLine { get; set; }

    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
}
