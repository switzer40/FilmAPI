using FilmAPI.Common.Constants;
using FilmAPI.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using FilmAPI.Common.DTOs;
using System.Linq;
using FilmAPI.Core.Interfaces;
using System.Net;

namespace FilmAPI.Tests.IntegrationTests.PersonController
{
    public class Get : TestBase
    {        
        private string _route;
        public Get() 
        {            
            _route = "/" + FilmConstants.PersonUri;
        }
        [Fact]
        public async Task ReturnsAudreyJuliaAndRichardAsync()
        {
            var l = await GetPersonAsync(_route);
            Assert.Equal(3, l.Count);
            Assert.True(l.Any(p => p.FirstMidName.Contains("Audrey")));
            Assert.True(l.Any(p => p.FirstMidName.Contains("Julia")));
            Assert.True(l.Any(p => p.FirstMidName.Contains("Richard")));
        }
        [Fact]
        public async Task ReturnsJuliaRobertsGivenValidData()
        {
            var firstMidName = "Julia";
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var p = await CompleteGetPersonAsync(lastName, birthdate, _route);
            Assert.Equal(lastName, p.LastName);
            Assert.Equal(birthdate, p.BirthdateString);
            Assert.Equal(firstMidName, p.FirstMidName);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenUnknownLastNameAsync()
        {
            var lastName = "Robertson";
            var birthdate = "1967-10-28";
            var response = await GetPersonAsync(lastName, birthdate, _route);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenUnknownBirthdateAsync()
        {
            var lastName = "Roberts";
            var birthdate = "1968-10-28";
            var response = await GetPersonAsync(lastName, birthdate, _route);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadQuestGivenInvalidKeyAsync()
        {
            var key = "Howdy";
            var response = await SimpleGetPersonWithKeyAsync(key, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
