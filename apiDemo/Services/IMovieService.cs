namespace apiDemo.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMovies_OrByGenreId(byte GenreId=0);
        Task<Movie> GetMovieById(int Id);
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<Movie> DeleteMovie(Movie movie);
    }

}
