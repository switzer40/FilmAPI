using FilmAPI.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FilmAPI.Tests.UnitTests
{
    public class PersonValidatorShould : ValidatorTestBase
    {
        [Fact]
        public void HaveErrorWhenLastNameIsEmpty()
        {
            var dto = new BasePersonDto("", "1940-01-29", "Leon");
            var results = PersonValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Last Name")));
        }
        [Fact]
        public void HaveErrorWhenBirthdateIsInvalid()
        {
            var dto = new BasePersonDto("McElroy", "1940-01-32", "Leon");
            var results = PersonValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Birthdate")));
        }
    }
}
