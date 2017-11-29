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

namespace FilmAPI.Tests.IntegrationTests.FilmPersonController
{
    public class Post : TestBase
    {
        private string _route;        
        public Post()
        {
            _route = "/" + FilmConstants.FilmPersonUri;
        }
        [Fact]
        public async Task ReturnsRichardGereAsActorinPrettyWoman()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var lastName = "Gere";
            var birthdate = "1949-08-31";
            var role = FilmConstants.Role_Actor;
            var dto = new BaseFilmPersonDto(title, year, lastName, birthdate, role);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_route, jsonContent);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var k = JsonConvert.DeserializeObject<KeyedFilmPersonDto>(stringResponse);
            
            Assert.Equal(title, k.Title);
            Assert.Equal(year, k.Year);
            Assert.Equal(lastName, k.LastName);
            Assert.Equal(birthdate, k.Birthdate);
            Assert.Equal(role, k.Role);
        }
    }
}
