// Importe os namespaces necessários no seu arquivo de controle da API

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeAPI.Models;
using AnimeAPI.Data.Dtos;
using System.Collections.Generic;
using AnimeAPI.Data;
using AutoMapper;

namespace AnimeAPI.Controllers
{
    [Route("api/animes")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly AnimeDbContext _context;
        private readonly IMapper _mapper;

        public AnimeController(AnimeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET api/animes
        [HttpGet]
        public ActionResult<IEnumerable<ReadAnimeDto>> GetAnimes()
        {
            var animes = _context.Animes.Include(a => a.Characters);
            return Ok(_mapper.Map<IEnumerable<ReadAnimeDto>>(animes));
        }

        // GET api/animes/{id}
        [HttpGet("{id}", Name = "GetAnimeById")]
        public ActionResult<ReadAnimeDto> GetAnimeById(int id)
        {
            var anime = _context.Animes.Include(a => a.Characters).FirstOrDefault(a => a.Id == id);

            if (anime == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReadAnimeDto>(anime));
        }

        // POST api/animes
        [HttpPost]
        public ActionResult<ReadAnimeDto> CreateAnime(CreateAnimeDto createAnimeDto)
        {
            var anime = _mapper.Map<Anime>(createAnimeDto);
            _context.Animes.Add(anime);
            _context.SaveChanges();

            var readAnimeDto = _mapper.Map<ReadAnimeDto>(anime);

            return CreatedAtRoute(nameof(GetAnimeById), new { Id = readAnimeDto.Id }, readAnimeDto);
        }

        // PUT api/animes/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateAnime(int id, UpdateAnimeDto updateAnimeDto)
        {
            var anime = _context.Animes.FirstOrDefault(a => a.Id == id);

            if (anime == null)
            {
                return NotFound();
            }

            _mapper.Map(updateAnimeDto, anime);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/animes/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAnime(int id)
        {
            var anime = _context.Animes.FirstOrDefault(a => a.Id == id);

            if (anime == null)
            {
                return NotFound();
            }

            _context.Animes.Remove(anime);
            _context.SaveChanges();

            return NoContent();
        }

        // GET api/animes/{animeId}/personagens
        [HttpGet("{animeId}/personagens")]
        public ActionResult<IEnumerable<ReadCharacterDto>> GetCharactersForAnime(int animeId)
        {
            var anime = _context.Animes.Include(a => a.Characters).FirstOrDefault(a => a.Id == animeId);

            if (anime == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ReadCharacterDto>>(anime.Characters));
        }

        // GET api/animes/{animeId}/personagens/{id}
        [HttpGet("{animeId}/personagens/{id}")]
        public ActionResult<ReadCharacterDto> GetCharacterForAnime(int animeId, int id)
        {
            var anime = _context.Animes.Include(a => a.Characters).FirstOrDefault(a => a.Id == animeId);

            if (anime == null)
            {
                return NotFound();
            }

            var character = anime.Characters.FirstOrDefault(c => c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReadCharacterDto>(character));
        }

        // POST api/animes/{animeId}/personagens
        [HttpPost("{animeId}/personagens")]
        public IActionResult CreateCharacterForAnime(int animeId, CreateCharacterDto createCharacterDto)
        {
            var anime = _context.Animes.FirstOrDefault(a => a.Id == animeId);

            if (anime == null)
            {
                return NotFound();
            }

            var character = _mapper.Map<Character>(createCharacterDto);
            character.Anime = anime;
            anime.Characters.Add(character);
            _context.SaveChanges();

            var readCharacterDto = _mapper.Map<ReadCharacterDto>(character);

            return CreatedAtAction(nameof(GetCharacterForAnime), new { animeId, id = readCharacterDto.Id }, readCharacterDto);
        }


        // PUT api/animes/{animeId}/personagens/{id}
        [HttpPut("{animeId}/personagens/{id}")]
        public IActionResult UpdateCharacterForAnime(int animeId, int id, UpdateCharacterDto updateCharacterDto)
        {
            var anime = _context.Animes.Include(a => a.Characters).FirstOrDefault(a => a.Id == animeId);

            if (anime == null)
            {
                return NotFound();
            }

            var character = anime.Characters.FirstOrDefault(c => c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCharacterDto, character);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/animes/{animeId}/personagens/{id}
        [HttpDelete("{animeId}/personagens/{id}")]
        public IActionResult DeleteCharacterForAnime(int animeId, int id)
        {
            var anime = _context.Animes.Include(a => a.Characters).FirstOrDefault(a => a.Id == animeId);

            if (anime == null)
            {
                return NotFound();
            }

            var character = anime.Characters.FirstOrDefault(c => c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
