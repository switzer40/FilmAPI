using FilmAPI.Core.Entities;
using FilmAPI.Infrastructure.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests
{
   
    public class Get : TestBase
    {
        private readonly HttpClient _client;

        public Get(FilmContext context) : base(context)
        {
            _client = base.GetClient();
        }

       [Fact]
       public async Task ReturnsListOfFilms()
        {
            var response = await _client.GetAsync("/api/films");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Film>>(stringResponse).ToList();

            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.Count(f => f.Title.Contains("Tiffany")));
            Assert.Equal(1, result.Count(f => f.Title.Contains("Woman")));
        }
    }
}
