using FilmAPI.Core.SharedKernel;
using FilmAPI.DTOs.Medium;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.MediaControllr
{
    public class Post : TestBase
    {
        private readonly HttpClient _client;

        public Post()
        {
            _client = base.GetClient();
        }
        [Fact]
        public async Task ReturnsOkGivenValidMedimDataAsync()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var type = FilmConstants.MediumType_BD;
            var key = _keyService.ConstructMediumSurrogateKey(title, year, type);
            var mediumToPost = new BaseMediumDto(title, year, type);
            var jsonString = JsonConvert.SerializeObject(mediumToPost);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            var response = _client.PostAsync("api/media", jsonContent).Result;
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<KeyedMediumDto>(stringResponse);

            Assert.Equal(title, model.Title);
            Assert.Equal(year, model.Year);
            Assert.Equal(type, model.MediumType);
        }
        [Fact]
        public async Task ReturnsOkGienorcedAddOnNewFilmAsync()
        {
            FilmConstants.Force = true;
            var title = "Robzilla";
            var year = (short)2017;
            var type = FilmConstants.MediumType_BD;
            var mediumToPost = new BaseMediumDto(title, year, type);
            var jsonString = JsonConvert.SerializeObject(mediumToPost);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/media", jsonContent);
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task ReturnsBadRequestGivenTooarlyYearAsync()
        {
            FilmConstants.Force = true;
            var title = "Robzilla";
            var year = (short)1849;
            var type = FilmConstants.MediumType_BD;
            var mediumToPost = new BaseMediumDto(title, year, type);
            var jsonString = JsonConvert.SerializeObject(mediumToPost);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/media", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenTooLateYearAsync()
        {
            FilmConstants.Force = true;
            var title = "Robzilla";
            var year = (short)2051;
            var type = FilmConstants.MediumType_BD;
            var mediumToPost = new BaseMediumDto(title, year, type);
            var jsonString = JsonConvert.SerializeObject(mediumToPost);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/media", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenEmptyTitleAsync()
        {
            FilmConstants.Force = true;
            var title = "";
            var year = (short)2051;
            var type = FilmConstants.MediumType_BD;
            var mediumToPost = new BaseMediumDto(title, year, type);
            var jsonString = JsonConvert.SerializeObject(mediumToPost);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/media", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}