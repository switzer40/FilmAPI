using FilmAPI.Core.SharedKernel;
using FilmAPI.DTOs;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using System.Text;

namespace FilmAPI.Tests.IntegrationTests.MediaController
{
    public class Post: TestBase
    {
        private readonly HttpClient _client;

        public Post()
        {
            _client = base.GetClient();
        }


        [Fact]
        public async Task ReturnsOkGivenValidMediumDataAsync()
        {
            string title = "MadMax";
            short year = 2017;
            var type = FilmConstants.MediumType_BD;
            var mediumToPost = new MediumDto(title, year, type);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/media", jsonContent);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<MediumDto>(stringResponse);

            Assert.Equal(title, model.Title);
            Assert.Equal(year, model.Year);
            Assert.Equal(type, model.MediumType);
        }
        [Fact]
        public async Task ReturnBadRequestGivenEmptyTitleAsync()
        {
            var emptyTitle = "";
            var goodYear = (short)1967;
            var goodMediumType = FilmConstants.MediumType_BD;
            var mediumToPost = new MediumDto(emptyTitle, goodYear, goodMediumType);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/media", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnBadRequestGivenTooEarlyYearAsync()
        {
            var goodTitle = "MadMax";
            var tooEarlyYear = (short)1849;
            var goodMediumType = FilmConstants.MediumType_BD;
            var mediumToPost = new MediumDto(goodTitle, tooEarlyYear, goodMediumType);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/media", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnBadRequestGivenTooLateYearAsync()
        {
            var goodTitle = "MadMax";
            var tooLateYear = (short)2051;
            var goodMediumType = FilmConstants.MediumType_BD;
            var mediumToPost = new MediumDto(goodTitle, tooLateYear, goodMediumType);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/media", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnBadRequestGivenInvalidMediumTypeAsync()
        {
            var goodTitle = "MadMax";
            var goodYear = (short)1957;
            var invaliddMediumType = "Tape";
            var mediumToPost = new MediumDto( goodTitle, goodYear, invaliddMediumType);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/media", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
