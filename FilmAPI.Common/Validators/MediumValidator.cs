using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Validators
{
    public class MediumValidator : AbstractValidator<BaseMediumDto>
    {
        public MediumValidator()
        {
            RuleFor(m => m.Title).NotNull().NotEmpty();
            RuleFor(m => m.Year).InclusiveBetween((short)1850, (short)2050);
            RuleFor(m => m.MediumType).NotNull().NotEmpty().Must(BeValidMediumType);
        }

        private bool BeValidMediumType(string arg)
        {
            return ((arg == FilmConstants.MediumType_BD) || (arg == FilmConstants.MediumType_DVD));
        }
    }
}
