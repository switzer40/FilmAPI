using FilmAPI.Core.SharedKernel;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentValidation.TestHelper;

namespace FilmAPI.Tests.UnitTests
{
    public class MediumValidatorValidateShould :  ValidatorTestBase
    {
        [Fact]
        public void FailGivenEmptyTitle()
        {
            var model = new MediumViewModel("", 1957,GoodMediumType);
            mediumValidator.ShouldHaveValidationErrorFor(m => m.FilmTitle, model);
        }
        [Fact]
        public void FailGivenToolongTitle()
        {
            var model = new MediumViewModel(LongString, 1957,GoodMediumType);
            mediumValidator.ShouldHaveValidationErrorFor(m => m.FilmTitle, model);
        }
        [Fact]
        public void FailGivenTooEarlyYear()
        {
            var model = new MediumViewModel(LongString, EarlyYear, GoodMediumType);
            mediumValidator.ShouldHaveValidationErrorFor(m => m.FilmYear, model);
        }
        [Fact]
        public void FailGivenTooLateYear()
        {
            var model = new MediumViewModel(LongString, LateYear, GoodMediumType);
            mediumValidator.ShouldHaveValidationErrorFor(m => m.FilmYear, model);
        }
        [Fact]
        public void FailGivenInvalidMediumType()
        {
            var model = new MediumViewModel(LongString, GoodYear, BadMediimType);
            mediumValidator.ShouldHaveValidationErrorFor(m => m.MediumType, model);
        }
    }
}
