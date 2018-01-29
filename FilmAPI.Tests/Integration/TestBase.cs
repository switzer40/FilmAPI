using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Common.Utilities;
using FilmAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace FilmAPI.Tests.Integration
{
    public class TestBase
    {
        public TestBase()
        {            
            _route = $"http://localhost:5000/api";
            _keyService = new KeyService();            
        }
               
        protected async Task PopulateFilmTestdataAsync()
        {
            await DeleteAsync("Film", "ClearAll");
            var title1 = "Frühstück bei Tiffany";
            var title2 = "Pretty Woman";
            var year1 = (short)1961;
            var year2 = (short)1990;
            var length1 = (short)110;
            var length2 = (short)109;
            await AddOneFilmAsync(title1, year1, length1);
            await AddOneFilmAsync(title2, year2, length2);
        }
        protected async Task PopulatePersonTestDataAsync()
        {
            await DeleteAsync("Person", "ClearAll");
            var lastName1 = "Hepburn";
            var lastName2 = "Roberts";
            var birthdate1 = "1929-05-04";
            var birthdate2 = "1867-10-28";
            var firstName1 = "Audrey";
            var firstName2 = "Julia";
            await AddOnePersonAsync(lastName1, birthdate1, firstName1);
            await AddOnePersonAsync(lastName2, birthdate2, firstName2);
        }
        protected async Task PopulateMediumTestataAsync()
        {
            await DeleteAsync("Medium", "ClearAll");
            var title1 = "Frühstück bei Tiffany";
            var title2 = "Pretty Woman";
            var year1 = (short)1961;
            var year2 = (short)1990;
            var mediumType1 = FilmConstants.MediumType_DVD;
            var mediumType2 = FilmConstants.MediumType_DVD;
            var location1 = FilmConstants.Location_Left;
            var location2 = FilmConstants.Location_Left;
            await AddOneMediumAsync(title1, year1, mediumType1, location1);
            await AddOneMediumAsync(title2, year2, mediumType2, location2);    
        }
        protected async Task PopulateRelationTestDataAsync()
        {
            await DeleteAsync("FilmPerson", "ClearAll");
            var title1 = "Frühstück bei Tiffany";
            var title2 = "Pretty Woman";
            var year1 = (short)1961;
            var year2 = (short)1990;
            var lastName1 = "Hepburn";
            var lastName2 = "Roberts";
            var birthdate1 = "1929-05-04";
            var birthdate2 = "1867-10-28";
            var role1 = FilmConstants.Role_Actor;
            var role2 = FilmConstants.Role_Actor;
            await AddOneRelationAsync(title1, year1, lastName1, birthdate1, role1);
            await AddOneRelationAsync(title2, year2, lastName2, birthdate2, role2);
        }

        private async Task AddOneRelationAsync(string title, short year, string lastName, string birthdate, string role)
        {
            var fp = new BaseFilmPersonDto(title, year, lastName, birthdate, role);
            var result = await PostAsync<OperationStatus, BaseFilmPersonDto>("FilmPerson", "Add", fp);
            Debug.Assert(result.Value == 0);
        }

        private async Task AddOneMediumAsync(string title, short year, string mediumType, string location)
        {
            var m = new BaseMediumDto(title, year, mediumType, location);
            var result = await PostAsync<OperationStatus, BaseMediumDto>("Medium", "Add", m);
            Debug.Assert(result.Value == 0);
        }

        private async Task AddOnePersonAsync(string lastName, string birthdate, string firstMidName)
        {
            var p = new BasePersonDto(lastName, birthdate, firstMidName);
            var result = await PostAsync<OperationStatus, BasePersonDto>("Person", "Add", p);
            Debug.Assert(result.Value == 0);
        }

        private async Task AddOneFilmAsync(string title, short year, short length)
        {
            var f = new BaseFilmDto(title, year, length);
            var result = await PostAsync<OperationResult, BaseFilmDto>("Film", "Add", f);
            Debug.Assert(result.Status.Value == 0);
        }

        protected static HttpClient _client = GetClient();        
        protected string _route;
        protected IKeyService _keyService;
        protected static HttpClient GetClient()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseEnvironment("Testing");

            var server = new TestServer(builder);
            var client = server.CreateClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        protected async Task<T> GetResultAsync<T>(string controller, string action, string arg = "")
        {
            var route = (string.IsNullOrEmpty(arg)) ? $"{_route}/{controller}/{action}" : $"{_route}/{controller}/{action}/{arg}";
            var response = await _client.GetAsync(route);
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringResponse);
        }
        protected async Task<R> PostAsync<R,T>(string controller, string action, T arg)
        {

            var jsonContent = new StringContent(JsonConvert.SerializeObject(arg), Encoding.UTF8, "application/json");
            var route = $"{_route}/{controller}/{action}";
            var response = await _client.PostAsync(route, jsonContent);
            var stringResponse = await response.Content.ReadAsStringAsync();
             var retVal = JsonConvert.DeserializeObject<R>(stringResponse);
            return retVal;
        }
        protected async Task<OperationStatus> DeleteAsync(string controller, string action, string key ="")
        {
            CancellationToken token;
            var route = (!string.IsNullOrEmpty(key)) ? $"{_route}/{controller}/{action}/{key}" : $"{_route}/{controller}/{action}";
            var response = await _client.DeleteAsync(route, token);
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OperationStatus>(stringResponse);
        }
    }
}
