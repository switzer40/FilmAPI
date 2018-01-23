using FilmAPI.Common.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Validation.Interfaces;
using FilmAPI.Common.Constants;

namespace FilmAPI.Validation.Validators
{
    public class MediumValidator : BaseValidator<BaseMediumDto>, IMediumValidator
    {
        public MediumValidator()
        {
            RuleFor(m => m.Title).NotNull().NotEmpty();
            RuleFor(m => m.Year).InclusiveBetween((short)1850, (short)2050);
            RuleFor(m => m.MediumType).NotNull().NotEmpty().Must(BeValidMediumType);
            RuleFor(m => m.Location).NotNull().NotEmpty().Must(BeValidLocation);
        }
    }
}
