using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.MediaController
{
    public class Get : TestBase
    {        
        private string _route;
        
 
        public Get()
        {            
            _route = "/" + FilmConstants.MediumUri;            
        }
        [Fact]        
        public async Task ReturnsDVDWithPrettyWoman()
        {            
            var title = "Pretty Woman";
            var year = (short)1990;
            var mediumType = FilmConstants.MediumType_DVD;
            var dto = new BaseMediumDto(title, year, mediumType);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var key = _keyService.ConstructMediumKey(title, year, mediumType);
            var response = await _client.GetAsync($"{_route}/{key}");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var m = JsonConvert.DeserializeObject<KeyedMediumDto>(stringResponse);
            Assert.NotNull(m);            
            Assert.Equal(title, m.Title);
            Assert.Equal(year, m.Year);
            Assert.Equal(mediumType, m.MediumType);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenWrongTitle()
        {
            var title = "Ugly Woman";
            var year = (short)1990;
            var mediumType = FilmConstants.MediumType_DVD;
            var dto = new BaseMediumDto(title, year, mediumType);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var key = _keyService.ConstructMediumKey(title, year, mediumType);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenWrongYear()
        {
            var title = "Pretty Woman";
            var year = (short)1991;
            var mediumType = FilmConstants.MediumType_DVD;
            var dto = new BaseMediumDto(title, year, mediumType);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var key = _keyService.ConstructMediumKey(title, year, mediumType);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenWrongMediumType()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var mediumType = FilmConstants.MediumType_BD;
            var dto = new BaseMediumDto(title, year, mediumType);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var key = _keyService.ConstructMediumKey(title, year, mediumType);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
