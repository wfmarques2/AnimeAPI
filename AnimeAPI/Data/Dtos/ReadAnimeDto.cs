using AnimeAPI.Models;

namespace AnimeAPI.Data.Dtos
{
    public class ReadAnimeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string? Description { get; set; }
    }
}
