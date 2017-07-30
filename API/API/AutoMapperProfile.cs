using API.ViewModels;
using AutoMapper;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Film, FilmViewModel>();
            CreateMap<FilmViewModel, Film>();
            CreateMap<Person, PersonViewModel>();
            CreateMap<PersonViewModel, Person>();
            CreateMap<Medium, MediumViewModel>();
            CreateMap<MediumViewModel, Medium>();
            CreateMap<FilmPerson, FilmPersonViewModel>();
            CreateMap<FilmPersonViewModel, FilmPerson>();
        }
    }
}
