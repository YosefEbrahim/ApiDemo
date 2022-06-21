namespace apiDemo.Dtos
{
    public class MovieDto
    {
        public String Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public String Storeline { get; set; }
        public IFormFile? Poster { get; set; }
        public byte GenreId { get; set; }
    }
}
