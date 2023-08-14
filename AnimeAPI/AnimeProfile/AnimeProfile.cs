using AnimeAPI.Data.Dtos;
using AnimeAPI.Models;
using AutoMapper;

namespace AnimeAPI.AnimeProfile
{
    public class AnimeProfile: Profile
    {
        public AnimeProfile() 
        {
            CreateMap<Anime, ReadAnimeDto>();
            CreateMap<CreateAnimeDto, Anime>();
            CreateMap<UpdateAnimeDto, Anime>();
            CreateMap<Anime, UpdateAnimeDto>();

            CreateMap<Character, ReadCharacterDto>();
            CreateMap<CreateCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
            CreateMap<Character, UpdateCharacterDto>();
        }
    }
}
