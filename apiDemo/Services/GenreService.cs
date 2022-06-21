namespace apiDemo.Services
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> CreateGenre(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            return genres;
        }

        public async Task<Genre> GetGenreById(byte Id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(n => n.Id == Id);
            return genre;
        }

        public Task<bool> IsValidGenre(byte Id)
        {
           return _context.Genres.AnyAsync(n => n.Id == Id);
  
        }

        public Genre UpdateGenre(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
            return genre;
        }
    }
}
