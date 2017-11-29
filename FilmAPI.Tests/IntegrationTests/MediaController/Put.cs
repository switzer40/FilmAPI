using FilmAPI.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FilmAPI.Common.DTOs;
using Xunit;
using System.Net.Http;
using Newtonsoft.Json;

namespace FilmAPI.Tests.IntegrationTests.MediaController
{
    public class Put : TestBase
    {
        private string _route;
        public Put()
        {
            _route = "/" + FilmConstants.MediumUri;
        }
        [Fact]
        public async Task UpdatesLocationCorrectly()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var mediumType = FilmConstants.MediumType_DVD;
            var location = FilmConstants.Location_Right;
            var dto = new BaseMediumDto(title, year, mediumType, location);
            var key = _keyService.ConstructMediumKey(title, year, mediumType);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_route, jsonContent);
            response.EnsureSuccessStatusCode();
            var response1 = await _client.GetAsync($"{_route}/{key}");
            response1.EnsureSuccessStatusCode();
            var stringResponse = await response1.Content.ReadAsStringAsync();
            var k = JsonConvert.DeserializeObject<KeyedMediumDto>(stringResponse);
            Assert.Equal(title, k.Title);
            Assert.Equal(year, k.Year);
            Assert.Equal(mediumType, k.MediumType);
            Assert.Equal(location, k.Location);
        }        
    }
}
