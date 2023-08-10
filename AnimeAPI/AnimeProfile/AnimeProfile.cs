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
        }
    }
}
