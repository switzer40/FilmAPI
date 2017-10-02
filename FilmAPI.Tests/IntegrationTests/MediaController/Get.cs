using FilmAPI.Core.SharedKernel;
using FilmAPI.DTOs;
using FilmAPI.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.MediaController
{
    public class Get : TestBase
    {
        private readonly HttpClient _client;


        public Get()
        {
            _client = base.GetClient();
            _keyService = new KeyService();
        }

        [Fact]
        public async Task ReturnsListOfMediaAsync()
        {
            var response = await _client.GetAsync("api/media");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<MediumDto>>(stringResponse);

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result.Where(m => m.Title.Contains("Tiffany")).Count());
            Assert.Equal(1, result.Where(m => m.Title.Contains("Woman")).Count());
        }
        [Fact]
        public async Task ReturnsBadRequestGivenNonexistentSurrogateKey()
        {
            string badKey = "Howdy";
            var response = await _client.GetAsync($"/api/media/{badKey}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnNotFoundGivenWrongTitelRightYearAndRightMediumAsync()
        {
            var wrongTitle = "Ugly Woman";
            var rightYear = (short)1990;
            var rightMedium = FilmConstants.MediumType_DVD;
            var badKey = _keyService.ConstructMediumSurrogateKey(wrongTitle, rightYear, rightMedium);
            var response = await _client.GetAsync($"/api/media/{badKey}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnNotFoundGienRightTitleWrongYearAndRightMediumAsync()
        {
            var rightTitle = "Pretty Woman";
            var wrongYear = (short)1991;
            var rightMedium = FilmConstants.MediumType_DVD;
            var badKey = _keyService.ConstructMediumSurrogateKey(rightTitle, wrongYear, rightMedium);
            var response = await _client.GetAsync($"/api/media/{badKey}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnNotFoundGivenRightTitelRightYearAndWrongMediumAsync()
        {
            var rightTitle = "Pretty Woman";
            var rightYear = (short)1990;
            var wrongMedium = FilmConstants.MediumType_BD;
            var badKey = _keyService.ConstructMediumSurrogateKey(rightTitle, rightYear, wrongMedium);
            var response = await _client.GetAsync($"/api/media/{badKey}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
