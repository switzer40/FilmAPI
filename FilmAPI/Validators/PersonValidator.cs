using FilmAPI.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Validators
{
    public class PersonValidator : BaseModelValidator<PersonViewModel>
    {
        public PersonValidator()
        {
            RuleFor(pvm => pvm.LastName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(pvm => pvm.BirthdateString).NotNull().NotEmpty().Must(BeAValidDate);
        }
    }
}



   