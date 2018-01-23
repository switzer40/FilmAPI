using FilmAPI.Common.DTOs;
using FilmAPI.Validation.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Validation.Validators
{
    public class PersonValidator : BaseValidator<BasePersonDto>, IPersonValidator
    {
        public PersonValidator()
        {
            RuleFor(p => p.LastName).NotNull().NotEmpty();
            RuleFor(p => p.Birthdate).NotNull().NotEmpty().Must(BeValidDate);
        }
    }
}
