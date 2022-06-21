namespace apiDemo.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies_OrByGenreId(byte GenreId = 0)
        {
            var movies = await _context.Movies
                             .Where(g=>g.GenreId==GenreId || GenreId==0)
                            .Include(g => g.Genre)
                            .OrderByDescending(g => g.Rate)
                            .ToListAsync();
            return movies;
        }

        public async Task<Movie> GetMovieById(int Id)
        {
            var movie = await _context.Movies.Include(g => g.Genre).FirstOrDefaultAsync(n => n.Id == Id);
            return movie;
        }
        public async Task<Movie> CreateMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();
            return movie;

        }

        public async Task<Movie> DeleteMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            _context.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
