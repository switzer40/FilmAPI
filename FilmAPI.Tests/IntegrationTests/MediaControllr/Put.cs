﻿using FilmAPI.Core.SharedKernel;
using FilmAPI.DTOs.Medium;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.MediaControllr
{
    public class Put : TestBase
    {
        private readonly HttpClient _client;

        public Put()
        {
            _client = base.GetClient();
        }
        [Fact]
        public async Task ReturnsOKGivenValidMediaDataAsync()
        {
            // Arrange
            // Start with a medium known to be in the DB.
            var title = "Pretty Woman";
            var year = (short)1990;
            var type = FilmConstants.MediumType_DVD;
            var newLocation = FilmConstants.Location_Right;
            var mediumToUpdate = new BaseMediumDto(title, year, type, newLocation);
            var key = _keyService.ConstructMediumSurrogateKey(title, year, type);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToUpdate), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"api/media", jsonContent);
            response.EnsureSuccessStatusCode();
            var response1 = await _client.GetAsync($"api/media/{key}");

            // Assert
            response1.EnsureSuccessStatusCode();

            // And now test whether it was properly updated
            var stringResponse = await response1.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedMediumDto>(stringResponse);
            Assert.Equal(newLocation, result.Location);
        }
    }
}
