using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmPersonController
{
    public class Get : TestBase
    {        
        private string _route;
        
        public Get()
        {            
            _route = "/" + FilmConstants.FilmPersonUri;     
        }
        [Fact]
        public async Task ReturnsTwoFilmPeopleAsync()
        {
            var response = await GetFilmPersonAsync(_route);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<KeyedFilmPersonDto>>(stringResponse);
            Assert.Equal(2, list.Count);

            foreach (var k in list)
            {                
                Assert.True(k.Title.Contains("Woman") || k.Title.Contains("Tiffany"));                
                Assert.True(k.LastName.Contains("Roberts") || k.LastName.Contains("Hepburn"));
            }           
        }
    }
}
