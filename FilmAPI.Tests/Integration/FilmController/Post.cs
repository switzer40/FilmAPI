using FilmAPI.Common.DTOs;
using FilmAPI.Common.Utilities;
using FilmAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FilmAPI.Tests.Integration.FilmController
{
    public class Post : TestBase
    {
        public Post() : base()
        {
        }
        [Fact]
        public async System.Threading.Tasks.Task AddReturnsPrettyWomanAsync()
        {
            // Arrange
            var title = "Avanti!";
            var year = (short)1972;
            var length = (short)140;
            var dto = new BaseFilmDto(title, year, length);

            // Act
            var result1 = await PostAsync<OperationStatus, BaseFilmDto>("Film", "Add", dto);
            var result2 = await GetResultAsync<KeyedFilmDto>("Film", "Result");

            // Assert
            Assert.Equal(OperationStatus.OK.Value, result1.Value);
            Assert.Equal(title, result2.Title);
            Assert.Equal(year, result2.Year);
            Assert.Equal(length, result2.Length);
        }
    }
}
