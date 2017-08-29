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
    public class MediumValidator : IMediumValidator
    {
        public CascadeMode CascadeMode { get => { return CascadeMode.StopOnFirstFailure}; }

        public bool CanValidateInstancesOfType(Type type)
        {
            return (type == typeof(MediumViewModel));
        }

        public IValidatorDescriptor CreateDescriptor()
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(MediumViewModel instance)
        {
            throw new NotImplementedException();
        }
        private ValidationResult ValidateWithTitleYearAndMediumType(string title, short year, string mediumType)
        {
            ValidationFailure titleIsNull = new ValidationFailure("FilmTitle", "may not be null", (title == null));
            ValidationFailure titleIsEmpty = new ValidationFailure("FilmTitle", "may not be the empty string", (title == ""));
            ValidationFailure titleIsTooLong = new ValidationFailure("FilmTitle", "may not have more than 50 chars", (title.Length > 50));
            ValidationFailure yearIsBefore1850 = new ValidationFailure("FilmYear", "may not be before 1850", (year < 1850));
            ValidationFailure yearIsAfter2100 = new ValidationFailure("FilmYear", "may not be after 2100", (year > 2100));
            ValidationFailure mediumTypeIsNull = new ValidationFailure("MediumType", "may not be nulll", (mediumType == null));
            ValidationFailure mediumTypeIsEmpty = new ValidationFailure("MediumType", "may not be the empty string", (mediumType == ""));
            ValidationFailure mediumTypIsInvalid = new ValidationFailure("MediumType", "must represent a valid type of medium", (!ValidMediumType(mediumType)));  
        }

        private bool ValidMediumType(string mediumType)
        {
            return (mediumType == "DVD" || mediumType == "BD");
        }

        public ValidationResult Validate(object instance)
        {
            string title = "";
            short year = 0;
            string mediumType = "";
            if (instance.GetType() == typeof(MediumViewModel))
            {
                MediumViewModel model = (MediumViewModel)instance;
                title = model.FilmTitle;
                year = model.FilmYear;
                mediumType = model.MediumType;
            }
            return ValidateWithTitleYearAndMediumType(title, year, mediumType);
        }

        public ValidationResult Validate(ValidationContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> ValidateAsync(MediumViewModel instance, CancellationToken cancellation = default(CancellationToken))
        {
            return await Task.Run(() => Validate(instance));
        }

        public async Task<ValidationResult> ValidateAsync(object instance, CancellationToken cancellation = default(CancellationToken))
        {
            return await Task.Run(() => Validate(instance));
        }

        public Task<ValidationResult> ValidateAsync(ValidationContext context, CancellationToken cancellation = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
