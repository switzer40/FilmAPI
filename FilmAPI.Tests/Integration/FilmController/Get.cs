using FilmAPI.Common.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.Integration.FilmController
{
    public class Get : TestBase
    {
        public Get() : base()
        {
        
        }
        [Fact]
        public async Task GetAllReturnsAnEmptyListAsync()    
        {
            var result = await GetResultAsync<List<KeyedFilmDto>>("Film", "GetAll");
            Assert.Empty(result);
        }
        [Fact]
        public async Task CountReturnsZero()
        {
            var result = await GetResultAsync<int>("Film", "Count");
            Assert.Equal(0, result);
        }
    }
}
