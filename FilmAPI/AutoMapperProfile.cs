using AutoMapper;
using FilmAPI.Core.Entities;
using FilmAPI.DTOs;
using FilmAPI.DTOs.Film;
using FilmAPI.DTOs.Medium;

namespace FilmAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Film, KeyedFilmDto>();
            CreateMap<KeyedFilmDto, Film>();
            CreateMap<BaseFilmDto, Film>();
            CreateMap<Medium, KeyedMediumDto>();
            CreateMap<KeyedMediumDto, Medium>();
            CreateMap<BaseMediumDto, Medium>();
        }            
    }
}
