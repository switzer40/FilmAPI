using FilmAPI.Core.Entities;
using FilmAPI.DTOs;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.PeopleController
{
    public class Post : TestBase
    {
        private readonly HttpClient _client;
        public Post()
        {
            _client = base.GetClient();
        }
        [Fact]
        public async Task ReturnsBadRequestGivenNoLastName()
        {
            
            var personToPost = new Person("", "2017-08-25");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/people", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnOkGivenValidPersonDataAsync()
        {
            string lastName = "Gere";
            string birthdate = "1949-08-30";
            var personToPost = new Person(lastName, birthdate , "Richard");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/people", jsonContent);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<PersonDto>(stringResponse);

            Assert.Equal(lastName, model.LastName);
            Assert.Equal(birthdate, model.Birthdate);
        }
        [Fact]
        public async Task ReturnBadRequestGivenInvalidBirthdateAsync()
        {
            string lastName = "Cooper";
            string birthdate = "1952-06-31";
            var personToPost = new Person(lastName, birthdate, "Gary");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/people", jsonContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnBadRequestGivenEmptyLastNameAsync()
        {
            string lastName = "";
            string birthdate = "1952-06-30";
            var personToPost = new Person(lastName, birthdate, "Gary");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/people", jsonContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
