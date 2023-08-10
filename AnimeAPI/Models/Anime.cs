using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace AnimeAPI.Models
{
    public class Anime
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int ReleaseYear { get; set; }

        public ICollection<Character> Characters { get; set; }

        public Anime()
        {
            Characters = new List<Character>();
        }
    }
}
