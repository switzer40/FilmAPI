using AutoMapper;
using FilmAPI.Core.Entities;
using FilmAPI.DTOs;
using FilmAPI.DTOs.Film;

namespace FilmAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Film, KeyedFilmDto>();
            CreateMap<KeyedFilmDto, Film>();
        }            
    }
}
