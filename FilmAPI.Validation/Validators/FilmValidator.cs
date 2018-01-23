using FilmAPI.Common.DTOs;
using FilmAPI.Validation.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Validation.Validators
{
    public class FilmValidator : BaseValidator<BaseFilmDto>, IFilmValidator
    {
        public FilmValidator()
        {
            RuleFor(f => f.Title).NotNull().NotEmpty();
            RuleFor(f => f.Year).InclusiveBetween((short)1850, (short)2050);
            RuleFor(f => f.Length).InclusiveBetween((short)10, (short)300);
        }
    }
}
