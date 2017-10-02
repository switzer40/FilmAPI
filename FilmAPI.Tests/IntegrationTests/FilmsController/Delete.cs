using FilmAPI.DTOs;
using FilmAPI.DTOs.Film;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmsController
{
    public class Delete : TestBase
    {
        private readonly HttpClient _client;

        public Delete()
        {
            _client = base.GetClient();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenRightTitelAndWrongYearAsync()
        {
            var rightTitel = "Pretty Woman";
            var wrongYear = (short)1991;
            string key = _keyService.ConstructFilmSurrogateKey(rightTitel, wrongYear);
            var response = await _client.DeleteAsync($"api/films/{key}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal(key, stringResponse);
        }
        [Fact]
        public async Task ReturnBadRequestGivenInvalidFilmSurrogateKeyAsync()
        {
            var key = "Howdy";
            var response = await _client.DeleteAsync($"api/films/{key}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal(key, stringResponse);

        }
        [Fact]
        public async Task ReturnMNotFoundGivenWrongTitleAndRightYearAsync()
        {
            var wrongTitel = "Ugly Woman";
            var rightYear = (short)1990;
            string key = _keyService.ConstructFilmSurrogateKey(wrongTitel, rightYear);
            var response = await _client.DeleteAsync($"api/films/{key}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal(key, stringResponse);
        }
        //[Fact]
        //public async Task ReturnsOkGivenValidSurrogateKey()
        //{
        //    var key = "Pretty Woman*1990";
        //    var response = await _client.GetAsync($"api/films/{key}");
        //    response.EnsureSuccessStatusCode();
        //    var stringResponse = await response.Content.ReadAsStringAsync();
        //    var model = JsonConvert.DeserializeObject<KeyedFilmDto>(stringResponse);
        //    var keyToDelete = (string.IsNullOrEmpty(model.SurrogateKey)) ? key : model.SurrogateKey;     
        //    keyToDelete = Uri.EscapeUriString(keyToDelete);
        //    var response2 = await _client.DeleteAsync($"api/films/{keyToDelete}");
        //    response2.EnsureSuccessStatusCode();
        //}                        
    }
}
