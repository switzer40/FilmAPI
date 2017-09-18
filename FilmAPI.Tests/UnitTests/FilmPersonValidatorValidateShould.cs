using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentValidation.TestHelper;
using FilmAPI.ViewModels;
using FilmAPI.Interfaces;
using FilmAPI.Services;

namespace FilmAPI.Tests.UnitTests
{
    public class FilmPersonValidatorValidateShould : ValidatorTestBase
    {        
        [Fact]
        public void FailGivenEmptyTitle()
        {
            var model = new FilmPersonViewModel("", GoodYear, "Fisher", GoodDateString, GoodRole);
            filmPersonValidator.ShouldHaveValidationErrorFor(m => m.FilmTitle, model);
        }
        [Fact]
        public void FailGivenTooLongTitle()
        {
            var model = new FilmPersonViewModel(LongString, GoodYear, GoodName, GoodDateString, GoodRole);
            filmPersonValidator.ShouldHaveValidationErrorFor(m => m.FilmTitle, model);
        }
        [Fact]
        public void failGivenEarlyYear()
        {
            var model = new FilmPersonViewModel(GoodTitle, EarlyYear, GoodName, GoodDateString, GoodRole);
            filmPersonValidator.ShouldHaveValidationErrorFor(m => m.FilmYear, model);
        }
        [Fact]
        public void FailGivenLateYear()
        {
            var model = new FilmPersonViewModel(GoodTitle, LateYear, GoodName, GoodDateString, GoodRole);
            filmPersonValidator.ShouldHaveValidationErrorFor(m => m.FilmYear, model);
        }
        [Fact]
        public void FailGivenEmptyName()
        {
            var model = new FilmPersonViewModel(GoodTitle, GoodYear, "", GoodDateString, GoodRole);
            filmPersonValidator.ShouldHaveValidationErrorFor(m => m.PersonLastName, model);
        }
        [Fact]
        public void FailGivenTooLongNameLame()
        {
            var model = new FilmPersonViewModel(GoodTitle, GoodYear, LongString, GoodDateString, GoodRole);
            filmPersonValidator.ShouldHaveValidationErrorFor(m => m.PersonLastName, model);
        }
        [Fact]
        public void FailGivenInvalidBirthdate()
        {
            var model = new FilmPersonViewModel(GoodTitle, GoodYear, GoodName, BadDateString, GoodRole);
            filmPersonValidator.ShouldHaveValidationErrorFor(m => m.PersonBirthdate, model);
        }
    }
}
