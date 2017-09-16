using FilmAPI.Services;
using FilmAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            var result = JsonConvert.DeserializeObject<List<MediumViewModel>>(stringResponse);

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result.Where(m => m.FilmTitle.Contains("Tiffany")).Count());
            Assert.Equal(1, result.Where(m => m.FilmTitle.Contains("Woman")).Count());
        }
    }
}
