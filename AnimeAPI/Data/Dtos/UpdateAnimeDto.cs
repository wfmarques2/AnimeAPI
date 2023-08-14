namespace AnimeAPI.Data.Dtos
{
    public class UpdateAnimeDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public int ReleaseYear { get; set; }
    }
}
