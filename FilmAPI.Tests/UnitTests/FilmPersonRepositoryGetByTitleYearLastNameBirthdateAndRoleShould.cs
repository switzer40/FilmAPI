using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FilmAPI.Tests.UnitTests
{
    public class FilmPersonRepositoryGetByTitleYearLastNameBirthdateAndRoleShould : TestBase
    {
        [Fact]
        public void ReturnCorrectFilmPerson()
        {
            // Arrange
            InitializeDatabase();

            // Act
            var fp = _filmPersonRepository.GetByTitleYearLastNameBirthdateAndRole(tiffanyTitle,
                                                                                  tiffanyYear,
                                                                                  hepburnLastName,
                                                                                  hepburnBirthdate,
                                                                                  actorRole);
        }
    }
}
