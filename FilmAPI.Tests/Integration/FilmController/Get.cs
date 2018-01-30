using FilmAPI.Common.DTOs;
using FilmAPI.Common.Utilities;
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
        public async Task GetAllReturnsTwoFilmsAsync()    
        {
            await PopulateFilmTestdataAsync();
            var result = await GetResultAsync<ListOperationResult>("Film", "GetAll");
            var films = result.ResultValue;
            Assert.Equal(2, films.Count);
        }
        [Fact]
        public async Task CountReturnsTwo()
        {
            await PopulateFilmTestdataAsync();
            var result = await GetResultAsync<ValueOperationResult>("Film", "Count");
            Assert.Equal(2, result.ReturnValue);
        }
        [Fact]
        public async Task ResultReturnsNull()
        {
            await PopulateFilmTestdataAsync();
            var result = await GetResultAsync<KeyedFilmDto>("Film", "Result");
            Assert.Null(result);
        }
    }
}
