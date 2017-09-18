using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentValidation.TestHelper;
using FilmAPI.Interfaces;
using FilmAPI.Services;

namespace FilmAPI.Tests.UnitTests
{
    public class FilmValidatorValidateShould : ValidatorTestBase
    {
        
        [Fact]
        public void FailGivenEmptyTitle()
        {
            var fvm = new FilmViewModel("", GoodYear);
            filmValidator.ShouldHaveValidationErrorFor(f => f.Title, fvm);
        }
        [Fact]
        public void FailGivenTooLongTitle()
        {
            var fvm = new FilmViewModel(LongString, GoodYear);
            filmValidator.ShouldHaveValidationErrorFor(f => f.Title, fvm);
        }
        [Fact]
        public void FailGivenTooEarlyYear()
        { 
            var fvm = new FilmViewModel(GoodTitle, EarlyYear);
            filmValidator.ShouldHaveValidationErrorFor(f => f.Year, fvm);
        }
        [Fact]
        public void FailGivenTooLateYear()
        {            
            var fvm = new FilmViewModel(GoodTitle, LateYear);
            filmValidator.ShouldHaveValidationErrorFor(f => f.Year, fvm);
        }
    }
}
