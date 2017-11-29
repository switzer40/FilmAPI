using FilmAPI.Common.DTOs;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FilmAPI.Tests.UnitTests
{
    public class FilmValidatorShould : ValidatorTestBase
    {
        [Fact]
        public void HaveErrorWhenTitleIsEmpty()
        {
            var dto = new BaseFilmDto("", GoodYear, (short)111);
            var results = FilmValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Title")));   
        }
        [Fact]
       public void HaveErrorWhenYearIsTooEarly()
        {
            var dto = new BaseFilmDto("Mad Max", (short)1849, (short)111);
            var results = FilmValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Year")));
        }
        [Fact]
        public void HaveErrorWhenYearIsTooLate()
        {
            var dto = new BaseFilmDto("Mad Max", (short)2051, (short)111);
            var results = FilmValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Year")));
        }
        [Fact]
        public void HaveErrorWhenLengthIsTooShort()
        {
            var dto = new BaseFilmDto("Mad Max", GoodYear, (short)7);
            var results = FilmValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Length")));
        }
        [Fact]
        public void HaveErrorWhenLengthIsTooLong()
        {
            var dto = new BaseFilmDto("Mad Max", GoodYear, (short)301);
            var results = FilmValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Length")));
        }
        [Fact]
        public void HaveNoErrorWhenInputIsValid()
        {
            var dto = new BaseFilmDto("StarTrek", (short)1994, (short)156);
            var results = FilmValidator.Validate(dto);
            Assert.True(results.IsValid);
        }
    }
}
