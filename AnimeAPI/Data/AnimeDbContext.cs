using AnimeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeAPI.Data
{
    public class AnimeDbContext : DbContext
    {
        public AnimeDbContext(DbContextOptions<AnimeDbContext> options) : base(options)
        { 
        }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Character> Characters { get; set; }

    }
}
