using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Validation.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Validation.Validators
{
    public class FilmPersonValidator : BaseValidator<BaseFilmPersonDto>, IFilmPersonValidator
    {
        public FilmPersonValidator()
        {
            RuleFor(fp => fp.Title).NotNull().NotEmpty();
            RuleFor(fp => fp.Year).InclusiveBetween((short)1850, (short)2050);
            RuleFor(fp => fp.LastName).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(fp => fp.Birthdate).Must(BeValidDate);
            RuleFor(fp => fp.Role).Must(BeValidRole);
        }
    }
}
