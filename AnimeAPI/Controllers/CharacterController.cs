using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeAPI.Data;
using AnimeAPI.Data.Dtos;
using AnimeAPI.Models;

namespace AnimeAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class CharacterController : ControllerBase
    {
        private readonly AnimeDbContext _context;

        public CharacterController(AnimeDbContext context)
        {
            _context = context;
        }

        // GET /api/animes/{animeId}/personagens
        [HttpGet("animes/{animeId}/personagens")]
        public async Task<ActionResult<IEnumerable<Character>>> GetAnimeCharacters(int animeId)
        {
            var anime = await _context.Animes.FindAsync(animeId);

            if (anime == null)
            {
                return NotFound();
            }

            var characters = anime.Characters.ToList();

            return Ok(characters);
        }

        // GET /api/animes/{animeId}/personagens/{id}
        [HttpGet("animes/{animeId}/personagens/{id}")]
        public async Task<ActionResult<Character>> GetAnimeCharacter(int animeId, int id)
        {
            var character = await _context.Characters
                .FirstOrDefaultAsync(c => c.AnimeId == animeId && c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        // POST /api/animes/{animeId}/personagens
        [HttpPost("animes/{animeId}/personagens")]
        public async Task<ActionResult<Character>> CreateAnimeCharacter(int animeId, Character character)
        {
            var anime = await _context.Animes.FindAsync(animeId);

            if (anime == null)
            {
                return NotFound();
            }

            character.Anime = anime;
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnimeCharacter), new { animeId = anime.Id, id = character.Id }, character);
        }

        // PUT /api/animes/{animeId}/personagens/{id}
        [HttpPut("animes/{animeId}/personagens/{id}")]
        public async Task<IActionResult> UpdateAnimeCharacter(int animeId, int id, Character character)
        {
            if (animeId != character.AnimeId || id != character.Id)
            {
                return BadRequest();
            }

            _context.Entry(character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(animeId, id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE /api/animes/{animeId}/personagens/{id}
        [HttpDelete("animes/{animeId}/personagens/{id}")]
        public async Task<IActionResult> DeleteAnimeCharacter(int animeId, int id)
        {
            var character = await _context.Characters
                .FirstOrDefaultAsync(c => c.AnimeId == animeId && c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CharacterExists(int animeId, int id)
        {
            return _context.Characters.Any(c => c.AnimeId == animeId && c.Id == id);
        }
    }
}
