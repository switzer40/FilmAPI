using FilmAPI.Core.SharedKernel;
using FilmAPI.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Validators
{
    public class MediumValidator : AbstractValidator<MediumViewModel>
    {
        public MediumValidator()
        {
            RuleFor(mvm => mvm.FilmTitle).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(mvm => mvm.FilmYear).ExclusiveBetween((short)1850, (short)2050);
            RuleFor(mvm => mvm.MediumType).NotNull().NotEmpty().Must(BeAValidMediumType);

        }
        private string[] _types = { FilmConstants.MediumType_BD, FilmConstants.MediumType_DVD };
        private bool BeAValidMediumType(string arg)
        {
            return _types.Contains(arg);
        }
    }
}
