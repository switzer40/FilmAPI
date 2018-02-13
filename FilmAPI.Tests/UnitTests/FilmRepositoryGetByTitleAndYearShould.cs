using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FilmAPI.Tests.UnitTests
{
    public class FilmRepositoryGetByTitleAndYearShould: TestBase
    {
        [Fact]
        public void ReturnTiffany()
        {
            // Arrange
            InitializeDatabase();
            var f = new Film(tiffanyTitle, tiffanyYear, tiffanyLength);

            // Act
            var (status, value) = _filmRepository.GetByTitleAndYear(tiffanyTitle, tiffanyYear);

            // Assert
            Assert.Equal(OperationStatus.OK, status);
            Assert.True(f.Equals(value));
        }
    }
}
