using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Validators
{
    public class FilmPersonValidator : AbstractValidator<BaseFilmPersonDto>
    {
        private readonly IDateValidator _dateValidator;
        public FilmPersonValidator(IDateValidator validator)
        {
            _dateValidator = validator;
            RuleFor(fp => fp.Title).NotNull().NotEmpty();
            RuleFor(fp => fp.Year).InclusiveBetween((short)1850, (short)2050);
            RuleFor(fp => fp.LastName).NotNull().NotEmpty();
            RuleFor(fp => fp.Birthdate).NotNull().NotEmpty().Must(BeValidDate);
            RuleFor(fp => fp.Role).NotNull().NotEmpty().Must(BeValidRole);
        }

        private bool BeValidRole(string arg)
        {
            return ((arg == FilmConstants.Role_Actor) ||
                    (arg == FilmConstants.Role_Composer) ||
                    (arg == FilmConstants.Role_Director) ||
                    (arg == FilmConstants.Role_Writer));
        }

        private bool BeValidDate(string arg)
        {
            _dateValidator.DateAsString = arg;
            return _dateValidator.Validate();
        }
    }
}
