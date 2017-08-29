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
    public class PersonValidator : IPersonValidator
    {
        public CascadeMode CascadeMode { get => { return CascadeMode.StopOnFirstFailure}; }

        public bool CanValidateInstancesOfType(Type type)
        {
            return (type == typeof(PersonViewModel));
        }

        public IValidatorDescriptor CreateDescriptor()
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(PersonViewModel instance)
        {
            throw new NotImplementedException();
        }
        private ValidationResult ValidateWithLastNameAndBirthdate(string lastName, string birthdate)
        {
            ValidationFailure lastNameIsNull = new ValidationFailure("LastName", "may not be null", (lastName == null));
            ValidationFailure lastNameIsEmpty = new ValidationFailure("LastName", "may not be the empty string", (lastName == ""));
            ValidationFailure lastNameIsTooLong = new ValidationFailure("LastName", "may not have more than 50 chars", (lastName.Length > 50));
            ValidationFailure birthdateIsNull = new ValidationFailure("BirthdateString", "may not be null", (birthdate == null));
            ValidationFailure birthdateIsEmpty = new ValidationFailure("BirthdateString", "may not be the empty string", (birthdate == ""));
            ValidationFailure birthdateIsInvalidDate = new ValidationFailure("BirthdateString", "must represent a valid date", (!IsValidDate(birthdate)));
            List<ValidationFailure> failures = new List<ValidationFailure>
            {
                lastNameIsNull,
                lastNameIsEmpty,
                lastNameIsTooLong,
                birthdateIsNull,
                birthdateIsEmpty,
                birthdateIsInvalidDate
            };
            return new ValidationResult(failures);
        }

        private bool IsValidDate(string date)
        {
            DateTime parsedDate = new DateTime();
            return DateTime.TryParse(date, out parsedDate);
        }

        public ValidationResult Validate(object instance)
        {
            string lastName = "";
            string birthdate = "";
            if (instance.GetType() == typeof(PersonViewModel))
            {
                PersonViewModel model = (PersonViewModel)instance;
                lastName = model.LastName;
                birthdate = model.BirthdateString;
            }
            return ValidateWithLastNameAndBirthdate(lastName, birthdate);
        }

        public ValidationResult Validate(ValidationContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> ValidateAsync(PersonViewModel instance, CancellationToken cancellation = default(CancellationToken))
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
