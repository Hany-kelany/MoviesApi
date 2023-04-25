using DevCreedApi2.DTO;
using DevCreedApi2.Models;
using DevCreedApi2.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevCreedApi2.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class GenreController : Controller
    {

        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllasync()
        {
            var Genre = await _genreService.Getall();
            return Ok(Genre);
        }

        [HttpPost]
        public async Task<IActionResult> Postasync(Createdto dto)
        {
            var Genre = new Genre()
            {
                Name = dto.Name,
            };
            await _genreService.Add(Genre);
            return Ok(Genre);
        }

        [HttpPut("id")]
        public async Task<IActionResult> EditAsync(int id, Createdto dto)
        {
            Genre Genre = _genreService.GetbyId(id);
            if (Genre == null)
                return NotFound($"not found movie with this id ={id}");
            Genre.Name = dto.Name;
            return Ok(Genre);
        }

        [HttpDelete]
        public async Task<IActionResult> Deleteasync(int id)
        {
            var Genre = _genreService.GetbyId(id);
            if (Genre == null)
                return NotFound($"not found movie with this id ={id}");
            _genreService.Delete(Genre);
            return Ok(Genre);
        }

    }
}
