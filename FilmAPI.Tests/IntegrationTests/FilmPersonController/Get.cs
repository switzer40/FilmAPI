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
        private readonly HttpClient _client;
        private string _route;
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        public Get(IFilmRepository frepo, IPersonRepository prepo)
        {
            _client = GetClient();
            _route = "/" + FilmConstants.FilmPersonUri;
            _filmRepository = frepo;
            _personRepository = prepo;
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
                var f = _filmRepository.GetByTitleAndYear(k.Title, k.Year);
                Assert.NotNull(f);
                Assert.True(f.Title.Contains("Woman") || f.Title.Contains("Tiffany"));
                var p = _personRepository.GetByLastNameAndBirthdate(k.LastName, k.Birthdate);
                Assert.NotNull(p);
                Assert.True(p.LastName.Contains("Roberts") || p.LastName.Contains("Hepburn"));
            }           
        }
    }
}
