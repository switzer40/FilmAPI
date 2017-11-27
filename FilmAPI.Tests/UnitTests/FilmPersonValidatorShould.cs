using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FilmAPI.Tests.UnitTests
{
    public class FilmPersonValidatorShould : ValidatorTestBase
    {
        [Fact]
        public void HaveErrorWhenTitleIsEmpty()
        {
            var dto = new BaseFilmPersonDto("",
                                            GoodYear,
                                            "Bryant",
                                            "1842-11-24",
                                            FilmConstants.Role_Composer);
            var results = FilmPersonValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Title")));
        }
        [Fact]
        public void HaveErrorWhenYearIsTooEarly()
        {
            var dto = new BaseFilmPersonDto("StarTrek",
                                            (short)1849,
                                            "Bryant",
                                            "1842-11-24",
                                            FilmConstants.Role_Composer);
            var results = FilmPersonValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Year")));
        }
        [Fact]
        public void HaveErrorWhenYearIsTooLate()
        {
            var dto = new BaseFilmPersonDto("StarTrek",
                                            (short)2051,
                                            "Bryant",
                                            "1842-11-24",
                                            FilmConstants.Role_Composer);
            var results = FilmPersonValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Year")));
        }
        [Fact]
        public void HaveErrorWhenLastNameIsEmpty()
        {
            var dto = new BaseFilmPersonDto("StarTrek",
                                            GoodYear,
                                            "",
                                            "1842-11-24",
                                            FilmConstants.Role_Composer);
            var results = FilmPersonValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Last Name")));
        }
        [Fact]
        public void HaveErrorWhenBirthdateIsInvalid()
        {
            var dto = new BaseFilmPersonDto("StarTrek",
                                            GoodYear,
                                            "Bryant",
                                            "1842-11-31",
                                            FilmConstants.Role_Composer);
            var results = FilmPersonValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Birthdate")));
        }
        [Fact]
        public void HaveErrorWhenRoleIsInvalid()
        {
            var dto = new BaseFilmPersonDto("StarTrek",
                                            GoodYear,
                                            "Bryant",
                                            "1842-11-30",
                                            "Producer");
            var results = FilmPersonValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Role")));
        }
        [Fact]
        public void HaveNoErrorWhenAllDataAreValid()
        {
            var dto = new BaseFilmPersonDto("StarTrek",
                                            GoodYear,
                                            "Bryant",
                                            "1842-11-30",
                                            FilmConstants.Role_Writer);
            var results = FilmPersonValidator.Validate(dto);
            Assert.True(results.IsValid);
        }
    }
}
