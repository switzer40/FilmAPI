using FilmAPI.Validators;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentValidation.TestHelper;

namespace FilmAPI.Tests.UnitTests
{
    public class PersobnValidatorValidateShould : ValidatorTestBase
    {
        [Fact]
        public void FailGivenEmptyLastName()
        {
            var pvm = new PersonViewModel("", "2017-08-31");
            
            personValidator.ShouldHaveValidationErrorFor(p => p.LastName, pvm);
        }
        [Fact]
        public void FailGivenLonglLastName()
        {
            var pvm = new PersonViewModel(LongString, GoodDateString);
            personValidator.ShouldHaveValidationErrorFor(p => p.LastName, pvm);
        }
        [Fact]
        public void FailGivenEmptyBirthdate()
        {
            var pvm = new PersonViewModel("Smith", "");

            personValidator.ShouldHaveValidationErrorFor(p => p.BirthdateString, pvm);
        }
        [Fact]
        public void FailGivenInvalidBirthdate()
        {
            var pvm = new PersonViewModel("Smith", BadDateString);

            personValidator.ShouldHaveValidationErrorFor(p => p.BirthdateString, pvm);
        }
    }
}
