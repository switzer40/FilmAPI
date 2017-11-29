using FilmAPI.Common.Constants;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.PersonController
{
    public class Post : TestBase
    {        
        private string _route;
        public Post()
        {            
            _route = "/" + FilmConstants.PersonUri;
        }
        [Fact]
        public async Task ReturnsCharlesBronsonGivenValidInput()
        {
            var lastName = "Bronson";
            var birthdate = "1921-11-03";
            var firstMidName = "Charles";
            var p = await CompletePostPersonAsync(lastName, birthdate, firstMidName, _route);
            Assert.Equal(lastName, p.LastName);
            Assert.Equal(birthdate, p.BirthdateString);
            Assert.Equal(firstMidName, p.FirstMidName);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenNoLastName()
        {
            var lastName = "";
            var birthdate = "1921-11-03";
            var firstMidName = "Charles";
            var response = await PostPersonAsync(lastName, birthdate, firstMidName, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}
