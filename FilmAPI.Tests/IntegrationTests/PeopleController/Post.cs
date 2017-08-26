﻿using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.PeopleController
{
    public class Post : TestBase
    {
        private readonly HttpClient _client;
        public Post()
        {
            _client = base.GetClient();
        }
        [Fact]
        public async Task ReturnsBadRequestGivenNoLastName()
        {
            
            var personToPost = new Person("", "2017-08-25");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/people", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkGivenValidPersonData()
        {
            string lastName = "Gere";
            var personToPost = new Person(lastName, "1949-07-30", "Richard");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToPost), Encoding.UTF7, "application/json");
            var response = await _client.PostAsync("api/people", jsonContent);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<PersonViewModel>(stringResponse);

            Assert.Equal(lastName, model.LastName);
        }
    }
}
