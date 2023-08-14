namespace AnimeAPI.Data.Dtos
{
    public class UpdateCharacterDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int AnimeId { get; set; }
    }
}
