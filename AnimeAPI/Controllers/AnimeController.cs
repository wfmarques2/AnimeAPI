using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeAPI.Data;
using AnimeAPI.Data.Dtos;
using AnimeAPI.Models;

namespace AnimeAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class AnimeController : ControllerBase
    {
        private readonly AnimeDbContext _context;

        public AnimeController(AnimeDbContext context)
        {
            _context = context;
        }

        // Endpoints para Anime

        // GET /api/animes
        [HttpGet("animes")]
        public async Task<ActionResult<IEnumerable<ReadAnimeDto>>> GetAnimes()
        {
            var animes = await _context.Animes
                .Select(anime => new ReadAnimeDto
                {
                    Id = anime.Id,
                    Name = anime.Name,
                    Description = anime.Description
                })
                .ToListAsync();

            return Ok(animes);
        }

        // GET /api/animes/{id}
        [HttpGet("animes/{id}")]
        public async Task<ActionResult<ReadAnimeDto>> GetAnime(int id)
        {
            var anime = await _context.Animes
                .Where(a => a.Id == id)
                .Select(anime => new ReadAnimeDto
                {
                    Id = anime.Id,
                    Name = anime.Name,
                    Description = anime.Description
                })
                .FirstOrDefaultAsync();

            if (anime == null)
            {
                return NotFound();
            }

            return Ok(anime);
        }

        // POST /api/animes
        [HttpPost("animes")]
        public async Task<ActionResult<ReadAnimeDto>> CreateAnime(CreateAnimeDto createAnimeDto)
        {
            var anime = new Anime
            {
                Name = createAnimeDto.Name,
                Description = createAnimeDto.Description,
                ReleaseYear = createAnimeDto.ReleaseYear
            };

            _context.Animes.Add(anime);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnime), new { id = anime.Id }, new ReadAnimeDto
            {
                Id = anime.Id,
                Name = anime.Name,
                Description = anime.Description
            });
        }

        // PUT /api/animes/{id}
        [HttpPut("animes/{id}")]
        public async Task<IActionResult> UpdateAnime(int id, CreateAnimeDto updateAnimeDto)
        {
            var anime = await _context.Animes.FindAsync(id);

            if (anime == null)
            {
                return NotFound();
            }

            anime.Name = updateAnimeDto.Name;
            anime.Description = updateAnimeDto.Description;
            anime.ReleaseYear = updateAnimeDto.ReleaseYear;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/animes/{id}
        [HttpDelete("animes/{id}")]
        public async Task<IActionResult> DeleteAnime(int id)
        {
            var anime = await _context.Animes.FindAsync(id);

            if (anime == null)
            {
                return NotFound();
            }

            _context.Animes.Remove(anime);
            await _context.SaveChangesAsync();

            return NoContent();
        }         
    }
}
