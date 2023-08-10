using AnimeAPI.Models;
namespace AnimeAPI.Data.Dtos
{
    public class CreateAnimeDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int ReleaseYear { get; set; }
    }
}
