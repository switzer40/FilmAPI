using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using System.Threading;

namespace FilmAPI.Validators
{
    public class FilmValidator : IFilmValidator
    {
        public CascadeMode CascadeMode { get => { return CascadeMode.StopOnFirstFailure; }; }

        public bool CanValidateInstancesOfType(Type type)
        {
            return (type == typeof(FilmViewModel);)
        }

        public IValidatorDescriptor CreateDescriptor()
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(FilmViewModel instance)
        {
            throw new NotImplementedException();
        }

        public ValidationResult ValidateAsync(object instance)
        {
            string title = "";
            short year= 0;
            if (instance.GetType() == typeof(FilmViewModel))
            {
                FilmViewModel model = (FilmViewModel)instance;
                title = model.Title;
                year = model.Year;                
            }
            return ValidateWithTitleAndYear(title, year);
        }
        private async Task<ValidationResult> ValidateWithTitleAndYearAsync(string title, short year)
        {
            ValidationFailure titileIsNull = new ValidationFailure("Title", "may not be null", (title == null);
            ValidationFailure titleIsEmpty = new ValidationFailure("Title", "may not be the empty string", (title == ""));
            ValidationFailure titleIsTooLong = new ValidationFailure("Title", "may not have more than 50 chars", (title.Length > 50));
            ValidationFailure yearIsBefore1850 = new ValidationFailure("Year", "may not be before 1850", (year < 1850));
            ValidationFailure yearIsAfter2100 = new ValidationFailure("Year", "may not be after 2100", (year > 2100));
            List<ValidationFailure> failures = new List<ValidationFailure>
                {
                    titileIsNull, titleIsEmpty, titleIsTooLong, yearIsBefore1850, yearIsAfter2100
                };
            return new ValidationResult(failures);
        }

        public ValidationResult Validate(ValidationContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> ValidateAsync(FilmViewModel instance, CancellationToken cancellation = default(CancellationToken))
        {
            return await ValidateWithTitleAndYearAsync(instance.Title, instance.Year);
        }

        public async Task<ValidationResult> ValidateAsync(object instance, CancellationToken cancellation = default(CancellationToken))
        {
            string title = "";
            short year = 0;
            if (instance.GetType() == typeof(FilmViewModel))
            {
                FilmViewModel model = (FilmViewModel)instance;
                title = model.Title;
                year = model.Year;
            }
            return await ValidateWithTitleAndYearAsync(title, year);
        }

        public Task<ValidationResult> ValidateAsync(ValidationContext context, CancellationToken cancellation = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
