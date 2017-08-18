using System;
using AutoMapper;
using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using FilmAPI.Core.Interfaces;
using FilmAPI.VviewModls;
using FilmAPI.Core.SharedKernel;

namespace FilmAPIdest.
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