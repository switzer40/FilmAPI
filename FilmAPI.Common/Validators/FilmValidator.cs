using FilmAPI.Common.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Validators
{
    public  class FilmValidator : AbstractValidator<BaseFilmDto>
    {
        public FilmValidator()
        {
            RuleFor(f => f.Title).NotEmpty();
            RuleFor(f => f.Year).LessThan((short)2050).GreaterThan((short)1850);
            RuleFor(f => f.Length).LessThan((short)300).GreaterThan((short)10);
        }
    }
}
