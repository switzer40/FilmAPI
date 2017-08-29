using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using System.Threading;
using FilmAPI.Core.SharedKernel;

namespace FilmAPI.Validators
{
    public class FilmPersonValidator : IFilmPersonValidator
    {
        public CascadeMode CascadeMode { get => { return CascadeMode.StopOnFirstFailure; }; }

        public bool CanValidateInstancesOfType(Type type)
        {
            return (type == typeof(FilmPersonViewModel));
        }
        public IValidatorDescriptor CreateDescriptor()
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(FilmPersonViewModel instance)
        {
            throw new NotImplementedException();
        }
        private ValidationResult ValidateWithTitleYearLastNameBirthdateAndRole(string title,
                                                                               short year,
                                                                               string lastName,
                                                                               string birthdate,
                                                                               string role)
        {
            ValidationFailure titleIsNull = new ValidationFailure("FilmTitle", "may not be null", (title == null));
            ValidationFailure titleIsEmpty = new ValidationFailure("FilmTitle", "may not bethe empty string", (title == ""));
            ValidationFailure titleIsTooLong = new ValidationFailure("FilmTitle", "may not have more than 50 chars", (title.Length > 50));
            ValidationFailure yearIsBefore1850 = new ValidationFailure("FilmYear", "may not be before 1850", (year < 1850));
            ValidationFailure yearIsAfter2100 = new ValidationFailure("FilmYear", "may not be after 2100", (year > 2100));
            ValidationFailure lastNameIsNull = new ValidationFailure("PersonLastName", "may not be null", (lastName == null));
            ValidationFailure lastNameIsEmpty = new ValidationFailure("PersonLastName", "may not be the empty string", (lastName == ""));
            ValidationFailure lastNameIsTooLong = new ValidationFailure("PersonLastName", "may not have more than 50 chars", (lastName.Length > 50));
            ValidationFailure birthdateIsNull = new ValidationFailure("PersonBirthdate", "may not be null", (birthdate == null));
            ValidationFailure birthdateIsEmpty = new ValidationFailure("PersonBirthdate", "may mot be the empty string", (birthdate == ""));
            ValidationFailure birthdateIsInvalidDate = new ValidationFailure("PersonBirthdate", "must represent a valid date", (!ValidDate(birthdate)));
            ValidationFailure roleIsUnknownRole = new ValidationFailure("Role", "must be a known role", (!KnownRole(role)));
            List<ValidationFailure> failures = new List<ValidationFailure>
            {
                titleIsNull,
                titleIsEmpty,
                titleIsTooLong,
                yearIsBefore1850,
                yearIsAfter2100,
                lastNameIsNull,
                lastNameIsEmpty,
                lastNameIsTooLong,
                birthdateIsNull,
                birthdateIsEmpty,
                birthdateIsInvalidDate,
                roleIsUnknownRole
            };
            return new ValidationResult(failures);
        }

        private bool KnownRole(string role)
        {
            List<string> knownRoles = new List<string>
            {
                FilmConstants.Role_Actor,
                FilmConstants.Role_Composer,
                FilmConstants.Role_Director,
                FilmConstants.Role_Writer
            };
            return knownRoles.Contains(role);
        }

        private bool ValidDate(string date)
        {
            DateTime parsedDate = new DateTime();
            return DateTime.TryParse(date, out parsedDate);
        }

        public ValidationResult Validate(object instance)
        {
            string title = "";
            short year = 0;
            string lastName = "";
            string birthdate = "";
            string role = "";
            if ((instance.GetType() == typeof(FilmPersonViewModel))
            {
                FilmPersonViewModel model = (FilmPersonViewModel)instance;
                title = model.FilmTitle;
                year = model.FilmYear;
                lastName = model.PersonLastName;
                birthdate = model.PersonBirthdate;
                role = model.Role;
            }
            return ValidateWithTitleYearLastNameBirthdateAndRole(title, year, lastName, birthdate, role);
        }

        public ValidationResult Validate(ValidationContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> ValidateAsync(FilmPersonViewModel instance, CancellationToken cancellation = default(CancellationToken))
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
