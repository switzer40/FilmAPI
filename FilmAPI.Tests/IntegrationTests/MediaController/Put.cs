using FilmAPI.Core.SharedKernel;
using FilmAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.MediaController
{
    public class Put : TestBase
    {
          private readonly HttpClient _client;

    public Put()
    {
        _client = base.GetClient();
    }

    [Fact]
    public async Task ReturnsOkGivenValidMediumData()
        {
            // Arrange
            var title = "Pretty Woman";
            var year = (short)1990;
            var type = FilmConstants.MediumType_DVD;
            var model = new MediumViewModel(title, year, type);
            var key = model.SurrogateKey;
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"api/media", jsonContent);
            response.EnsureSuccessStatusCode();

            var response1 = await _client.GetAsync($"api/nedia/{key}");

            // Assert
            response1.EnsureSuccessStatusCode();
        }
}
}
