using AutoMapper;
using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI
{
    public  class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Film, FilmViewModel>();
        }
        
    }
}
