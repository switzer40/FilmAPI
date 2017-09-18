using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using Xunit;

namespace FilmAPI.Tests.UnitTests
{
    public class KeyServiceDeconstructFilmSurrogateKeyShould : KeyTestBase
    {
        public KeyServiceDeconstructFilmSurrogateKeyShould() : base()
        {
        }
        [Fact]
        public void BeInverseToConstructFilmKey()
        {
            // Arrange
            string title = "Mad Max";
            short year = 2016;
            string key = _keyService.ConstructFilmSurrogateKey(title, year);
            FilmViewModel m = new FilmViewModel(new Film(title, year));

            //Act 
            (string FilmTitle, short FilmYear) = _keyService.DeconstructFilmSurrogateKey(key);

            //Assert
            Assert.Equal(title, FilmTitle);
            Assert.Equal(year, FilmYear);
        }  
    }
}
