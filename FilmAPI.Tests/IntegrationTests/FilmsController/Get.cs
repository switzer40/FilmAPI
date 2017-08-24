﻿using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmsController
{
    public class Get : TestBase
    {
        private readonly HttpClient _client;

        public Get()
        {
            _client = base.GetClient();
        }

        [Fact]
        public async Task ReturnsListOfFilms()
        {
            var response = await _client.GetAsync("/api/films");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<FilmViewModel>>(stringResponse);

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result.Where(f => f.Title.Contains("Tiffany")).Count());
            Assert.Equal(1, result.Where(f => f.Title.Contains("Woman")).Count());
        }
    }
}
