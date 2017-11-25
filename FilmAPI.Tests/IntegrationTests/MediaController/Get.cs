using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.MediaController
{
    public class Get : TestBase
    {
        private readonly HttpClient _client;
        private string _route;
        private IFilmRepository _filmRepository;
 
        public Get()
        {
            _client = GetClient();
            _route = "/" + FilmConstants.MediumUri;            
        }
        [Theory]
        [InlineData("frepo")]
        public async Task ReturnsDVDWithPrettyWoman(IFilmRepository frepo)
        {
            _filmRepository = frepo;
            var title = "Pretty Woman";
            var year = (short)1990;
            var mediumType = FilmConstants.MediumType_DVD;
            var response = await GetMediumAsync(title, year, mediumType, _route);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var m = JsonConvert.DeserializeObject<KeyedMediumDto>(stringResponse);
            Assert.NotNull(m);
            var f = _filmRepository.GetByTitleAndYear(m.Title, m.Year);
            Assert.NotNull(f);
            Assert.Equal(title, f.Title);
            Assert.Equal(year, f.Year);
        }
    }
}
