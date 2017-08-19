using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using Xunit;

namespace FilmAPI.Tests.UnitTests
{
    public class KeyServiceDeconstructPersonSurrogateKeyShould : KeyTestBase
    {
        [Fact]
        public void BeInverseToConstrtPersonSurrogateKey()
        {
            // Arrange
            string lastName = "Gibson";
            string birthdate = "1949-12-13";
            PersonViewModel m = new PersonViewModel(new Person(lastName, birthdate));

            // Act
            string key = _keyService.ConstructPersonSurrogateKey(m);
            _keyService.DeconstructPesonSurrogateKey(key);

            // Assert
            Assert.Equal(lastName, _keyService.PersonLastName);
            Assert.Equal(birthdate, _keyService.PersonBirthdate);
        }
    }
}