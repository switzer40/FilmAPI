using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FilmAPI.Tests.UnitTests
{
    public class MediumValidatorShould : ValidatorTestBase
    {
        [Fact]
        public void HaveErrorWhenTitleIsEmpty()
        {
            var dto = new BaseMediumDto("",
                                        GoodYear,
                                        FilmConstants.MediumType_DVD,
                                        FilmConstants.Location_Left);
            var results = MediumValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Title")));
        }
        [Fact]
        public void HaveErrorWhenYearIsTooEarly()
        {
            var dto = new BaseMediumDto("StarTrek",
                                        (short)1849,
                                        FilmConstants.MediumType_DVD,
                                        FilmConstants.Location_Left);
            var results = MediumValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Year")));
        }
        [Fact]
        public void HaveErrorWhenYearIsTooLate()
        {
            var dto = new BaseMediumDto("StarTrek",
                                        (short)2051,
                                        FilmConstants.MediumType_DVD,
                                        FilmConstants.Location_Left);
            var results = MediumValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Year")));
        }
        [Fact]
        public void HaveErrorWhenMediumTypeIsInvalid()
        {
            var dto = new BaseMediumDto("StarTrek",
                                        GoodYear,
                                        "Disk",
                                        FilmConstants.Location_Left);
            var results = MediumValidator.Validate(dto);
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Any(e => e.ErrorMessage.Contains("Medium")));
        }
        [Fact]
        public void HaveNoErrorWhenInputIsValid()
        {
            var dto = new BaseMediumDto("StarTrek",
                                        GoodYear,
                                        FilmConstants.MediumType_BD,
                                        FilmConstants.Location_Left);
            var results = MediumValidator.Validate(dto);
            Assert.True(results.IsValid);
        }
    }
}
