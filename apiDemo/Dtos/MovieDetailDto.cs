﻿namespace apiDemo.Dtos
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string Storeline { get; set; }
        public byte[] Poster { get; set; }
        public byte GenreId { get; set; }
        public String  GenreName { get; set; }
    }
}
