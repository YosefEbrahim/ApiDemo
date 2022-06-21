using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        public MoviesController(IMovieService movieService, IGenreService genreService, IMapper mapper)
        {
            _movieService = movieService;
            _genreService = genreService;
            _mapper = mapper;
        }
        private List<string> _allowedExtensions=new List<string>() { ".jpg",".png"};
        private long _maxAllowedPosterSize = 1048576;
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
         var movies =await _movieService.GetAllMovies_OrByGenreId();
            var data = _mapper.Map<IEnumerable<MovieDetailDto>>(movies);
            return Ok(data);

        }
        [HttpGet(template:"{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            var movie =await _movieService.GetMovieById(Id);
            if (movie == null)
                return NotFound(value: "movie not found");

            var dto = _mapper.Map<MovieDetailDto>(movie);
            return Ok(dto);
        }

        [HttpGet(template: "GetByGenreId/{GenreId}")]
        public async Task<IActionResult> GetByGenreIdAsync(byte GenreId)
        {
            var movies = await _movieService.GetAllMovies_OrByGenreId(GenreId);
            var data = _mapper.Map<IEnumerable<MovieDetailDto>>(movies);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]MovieDto dto)
        {
            if (dto.Poster == null)
                return BadRequest(error: "Poster is Required");
            if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest(error: "only .png and .jpg are allowed");
            if(dto.Poster.Length>_maxAllowedPosterSize)
                return BadRequest(error: "the maxmuim size allowed is 1mb");
            var isVlaidGenre = await _genreService.IsValidGenre(dto.GenreId); 
            if(!isVlaidGenre)
                return BadRequest(error: "Invalid Genre Id");

            using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);

            Movie movie = _mapper.Map<Movie>(dto);
            movie.Poster = dataStream.ToArray();
          await  _movieService.CreateMovie(movie);
            return Ok(movie);
            
        }

        [HttpPut(template:"{Id}")]
        public async Task<IActionResult> UpdateAsync(int Id,[FromForm] MovieDto dto)
        {
            var movie = await _movieService.GetMovieById(Id);
            if (movie == null)
                return NotFound(value: $"the movie with this is id:{Id} was not found");
            var isVlaidGenre = await _genreService.IsValidGenre(dto.GenreId);
            if (!isVlaidGenre)
                return BadRequest(error: "Invalid Genre Id");
            if (dto.Poster != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest(error: "only .png and .jpg are allowed");
                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest(error: "the maxmuim size allowed is 1mb");
                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                movie.Poster = dataStream.ToArray();
            }
            movie.Storeline=dto.Storeline;
            movie.GenreId=dto.GenreId;
            movie.Rate=dto.Rate;
            movie.Title=dto.Title;
            movie.Year=dto.Year;
            await _movieService.UpdateMovie(movie);
            return Ok(movie);

        }


        [HttpDelete(template:"{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var movie = await _movieService.GetMovieById(Id);
            if (movie == null)
                return NotFound(value: $"the movie with this is id:{Id} was not found");

            await  _movieService.DeleteMovie(movie);
            return Ok(movie);

        }
    }
}
