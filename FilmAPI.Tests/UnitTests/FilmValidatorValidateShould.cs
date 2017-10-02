using Xunit;
using FluentValidation.TestHelper;
using FilmAPI.DTOs;
using FilmAPI.DTOs.Film;

namespace FilmAPI.Tests.UnitTests
{
    public class FilmValidatorValidateShould : ValidatorTestBase
    {
        
        [Fact]
        public void FailGivenEmptyTitle()
        {
            var fvm = new KeyedFilmDto("", GoodYear);
            filmValidator.ShouldHaveValidationErrorFor(f => f.Title, fvm);
        }
        [Fact]
        public void FailGivenTooLongTitle()
        {
            var fvm = new KeyedFilmDto(LongString, GoodYear);
            filmValidator.ShouldHaveValidationErrorFor(f => f.Title, fvm);
        }
        [Fact]
        public void FailGivenTooEarlyYear()
        { 
            var fvm = new KeyedFilmDto(GoodTitle, EarlyYear);
            filmValidator.ShouldHaveValidationErrorFor(f => f.Year, fvm);
        }
        [Fact]
        public void FailGivenTooLateYear()
        {            
            var kfd = new KeyedFilmDto(GoodTitle, LateYear);
            filmValidator.ShouldHaveValidationErrorFor(f => f.Year, kfd);
        }
    }
}
