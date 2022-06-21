using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async  Task<IActionResult> GetAllAsync()
        {
            var Genres = await _genreService.GetAll();
            return Ok(Genres);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGenreAsync([FromBody]GenreDto dto)
        {
            var genre =new Genre() { Name=dto.Name};
            await _genreService.CreateGenre(genre);
            return Ok(genre);   
        }
        [HttpPut(template:"{id}")]
        public async Task<IActionResult> UpdateAsync(byte Id,[FromBody] GenreDto dto)
        {
            var genre = await _genreService.GetGenreById(Id);
            if (genre == null)
                return NotFound(value:$"No genre was found with id: {Id}");
            genre.Name=dto.Name;
            _genreService.UpdateGenre(genre);
            return Ok(genre);

        }
        [HttpDelete(template: "{id}")]
       public async Task<IActionResult> DeleteAsync(byte Id)
        {
            var genre = await _genreService.GetGenreById(Id);
            if (genre == null)
                return NotFound(value: $"No genre was found with id: {Id}");
            _genreService.DeleteGenre(genre);
            return  Ok(genre);    
        }

    }
}
