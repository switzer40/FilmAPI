using FilmAPI.Core.SharedKernel;
using FilmAPI.DTOs.Person;
using FilmAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.PeopleController
{
    public class Post : TestBase
    {
        private readonly HttpClient _client;
        private string _route;
        public Post()
        {
            _client = base.GetClient();
            _keyService = new KeyService();
            _route = FilmConstants.PersonUri;
        }
        [Fact]
        public async Task ReturnOkGivenValidPersonData()
        {
            var firstName = "Rowan";
            var lastName = "Miller";
            var birthdate = "1950-4-19";
            var personToPost = new BasePersonDto(lastName, birthdate, firstName);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_route, jsonContent);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedPersonDto>(stringResponse);

            Assert.Equal(firstName, result.FirstMidName);
            Assert.Equal(lastName, result.LastName);
            Assert.Equal(birthdate, result.Birthdate);
        }
    }
}
