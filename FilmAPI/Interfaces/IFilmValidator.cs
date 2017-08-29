using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace FilmAPI.Interfaces
{
    public interface IFilmValidator : IValidator<FilmViewModel>
    {
    }
}
