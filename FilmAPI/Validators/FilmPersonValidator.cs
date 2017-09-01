using FilmAPI.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Validators
{
    public class FilmPersonValidator : BaseModelValidator<FilmPersonViewModel>
    {
        public FilmPersonValidator()
        {
            RuleFor(model => model.FilmTitle).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(model => model.FilmYear).ExclusiveBetween((short)1850, (short)2050);
            RuleFor(model => model.PersonLastName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(model => model.PersonBirthdate).NotNull().NotEmpty().Must(BeAValidDate);
            RuleFor(model => model.Role).NotNull().NotEmpty().Must(BeAValidRole);
        }
    }
}
