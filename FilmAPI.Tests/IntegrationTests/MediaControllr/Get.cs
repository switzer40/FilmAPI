using FilmAPI.DTOs.Medium;
using FilmAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.MediaControllr
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
        public async System.Threading.Tasks.Task ReturnsListOfMediaAsync()
        {
            var response = await _client.GetAsync("api/media");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<KeyedMediumDto>>(stringResponse);

            Assert.Equal(2, result.Count);
        }
    }
}
