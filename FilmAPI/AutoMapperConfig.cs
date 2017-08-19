using AutoMapper;
using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;

namespace FilmAPI
{
    public  class AutoMapperConfig : Profile
    {        
        public AutoMapperConfig()
        {
            CreateMap<Film, FilmViewModel>();
            CreateMap<Person, PersonViewModel>();
            //CreateMap<FilmPerson, FilmPersonViewModel>();
            //CreateMap<Medium, MediumViewModel>();
        }     
    }
}