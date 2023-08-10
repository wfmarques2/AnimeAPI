using AnimeAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace AnimeAPI.Models
{
    public class Character
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public Anime Anime { get; set; }

        public int AnimeId { get; set; }


    }
}
