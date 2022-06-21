namespace apiDemo.Services
{
    public interface IGenreService
    {

        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetGenreById(byte Id);
        Task<Genre> CreateGenre(Genre genre);
        Genre UpdateGenre(Genre genre);
        Genre DeleteGenre(Genre genre);
       Task <bool> IsValidGenre(byte Id);

    }
}
