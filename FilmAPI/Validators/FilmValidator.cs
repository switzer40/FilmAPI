using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using FluentValidation;

namespace FilmAPI.Validators
{
    public class FilmValidator : BaseModelValidator<FilmViewModel>
    {
        public FilmValidator()
        {
            RuleFor(fvm => fvm.Year).ExclusiveBetween((short)1850, (short)2050);
            RuleFor(fvm => fvm.Title).NotNull().NotEmpty().MaximumLength(50);
        }
    }    
}
