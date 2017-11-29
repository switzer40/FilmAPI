using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Validators
{
    public class PersonValidator : AbstractValidator<BasePersonDto>
    {
        private readonly IDateValidator _dateValidator;
        public PersonValidator(IDateValidator validator)
        {
            _dateValidator = validator;
            RuleFor(p => p.LastName).NotNull().NotEmpty();
            RuleFor(p => p.Birthdate).NotNull().NotEmpty().Must(BeValidDate);
            RuleFor(p => p.FirstMidName).NotNull();
        }
        
        private bool BeValidDate(string arg)
        {
            _dateValidator.DateAsString = arg;
            return _dateValidator.Validate();
        }
    }
}
