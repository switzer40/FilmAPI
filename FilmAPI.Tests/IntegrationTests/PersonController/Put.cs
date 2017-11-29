using FilmAPI.Common.Constants;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.PersonController
{
    public class Put : TestBase
    {
        private string _route;
        public Put()
        {
            _route = "/" + FilmConstants.PersonUri;
        }
        [Fact]
        public async Task ReturnsBadRequestGivenEmptyLastNameAsync()
        {
            var lastName = "";
            var birthdate = "1942-03-29";
            var firstMidName = "Sally";
            var response = await PutPersonAsync(lastName, birthdate, firstMidName, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);            
        }
        [Fact]
        public async Task ReturnsBadRequestGivenInvalidBirthdayAsync()
        {
            var lastName = "Kirkman";
            var birthdate = "1942-03-32";
            var firstMidName = "Sally";
            var response = await PutPersonAsync(lastName, birthdate, firstMidName, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task UpdatesFirstNameCorrectlyAsync()
        {
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var firstMidName = "Juliet";
            var key = _keyService.ConstructPersonKey(lastName, birthdate);
            var response = await PutPersonAsync(lastName, birthdate, firstMidName, _route);
            response.EnsureSuccessStatusCode();
            var p = await GetPersonWithKeyAsync(key, _route);
            Assert.Equal(lastName, p.LastName);
            Assert.Equal(birthdate, p.BirthdateString);
            Assert.Equal(firstMidName, p.FirstMidName);
        }
    }
}
