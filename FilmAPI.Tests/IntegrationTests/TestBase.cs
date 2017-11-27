using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Core.Entities;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using FilmAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System;
using FilmAPI.Core.Interfaces;

namespace FilmAPI.Tests.IntegrationTests
{
    public class TestBase
    {
        protected IKeyService _keyService;        
        public TestBase()
        {            
            _keyService = new KeyService();
        }
        protected HttpClient GetClient()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
#pragma warning disable CS0436 // Type conflicts with imported type
                .UseStartup<Startup>()

                .UseEnvironment("Testing");
            var server = new TestServer(builder);
            var client = server.CreateClient();

            // Populate the Database
            //FilmInitializer.Seed(_context);


            // client always expects json results
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;                                  
        }
        // Access vertical slice Film
        protected async Task<List<Film>> GetFilmAsync(string route)
        {
            var response = await GetClient().GetAsync(route);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject < List < KeyedFilmDto >> (stringResponse);
            var result = new List<Film>();
            foreach (var k in list)
            {
                var f = (KeyedFilmDto)k;
                result.Add(new Film(f.Title, f.Year, f.Length));
            }
            return result;
        }
        protected async Task<HttpResponseMessage> GetFilmAsync(string title, short year, string route)
        {
            var key = _keyService.ConstructFilmKey(title, year);
            return await GetClient().GetAsync($"{route}/{key}");
        }
        protected async Task<Film> CompleteGetFilmAsync(string title, short year, string route)
        {
            var response = await GetFilmAsync(title, year, route);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedFilmDto>(stringResponse);
            return new Film(result.Title, result.Year, result.Length);
        }
        protected async Task<HttpResponseMessage> PostFilmAsync(string title, short year, short length, string route)
        {
            var filmToPost = new BaseFilmDto(title, year, length);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPost), Encoding.UTF8, "application/json");
            return await GetClient().PostAsync(route, jsonContent);
        }
        protected async Task<Film> CompletePostFilmAsync(string title, short year, short length, string route)
        {
            var response = await PostFilmAsync(title, year, length, route);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedFilmDto>(stringResponse);
            return new Film(result.Title, result.Year, result.Length);
        }
        protected async Task<HttpResponseMessage> PutFilmAsync(string title, short year, short length, string route)
        {
            var filmToPut = new BaseFilmDto(title, year, length);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPut), Encoding.UTF8, "application/json");
            return await GetClient().PutAsync(route, jsonContent);
        }
        protected async Task<HttpResponseMessage> DeleteFilmAsync(string title, short year, string route)
        {
            var key = _keyService.ConstructFilmKey(title, year);
            return await DeleteFilmWithKeyAsync(key, route);
        }
        protected async Task<HttpResponseMessage> DeleteFilmWithKeyAsync(string key, string route)
        {
            return await GetClient().DeleteAsync($"{route}/{key}");
        }
        // Access vertical slice FilmPerson
        protected async Task<HttpResponseMessage> GetFilmPersonAsync(string route)
        {
            return await GetClient().GetAsync(route);
        }
        protected async Task<HttpResponseMessage> GetFilmPersonAsync(string title, short year, string lastName, string birthdate, string role, string route)
        {
            var key = _keyService.ConstructFilmPersonKey(title, year, lastName, birthdate, role);
            return await GetClient().GetAsync($"{route}/{key}");
        }
        protected async Task<HttpResponseMessage> PostFilmPersonAsync(string title, short year, string lastName, string birthdate, string role, string route)
        {            
            var fpToAdd = new BaseFilmPersonDto(title, year, lastName, birthdate, role);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(fpToAdd), Encoding.UTF8, "application/json");
            return await GetClient().PostAsync(route, jsonContent);
        }
        protected async Task<HttpResponseMessage> PutFilmPersonAsync(string title, short year, string lastname, string birthdate, string role, string route)
        {
            var filmPersonToPut = new BaseFilmPersonDto(title, year, lastname, birthdate, role);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmPersonToPut), Encoding.UTF8, "application/json");
            return await GetClient().PutAsync(route, jsonContent);
        }
        protected async Task<HttpResponseMessage> DeleteFilmPersonAsync(string title, short year, string lastName, string birthdate,string role, string route)
        {
            var key = _keyService.ConstructFilmPersonKey(title, year, lastName, birthdate, role);
            return await GetClient().DeleteAsync($"{route}/{key}");
        }
        // Access vertical slice Medium
        
        
        protected async Task<HttpResponseMessage> GetMediumAsync(string title, short year, string mediumType, string route)
        {
            var key = _keyService.ConstructMediumKey(title, year, mediumType);
            return await GetClient().GetAsync($"{route}/{key}");
        }
        protected async Task<HttpResponseMessage> PostMediumAsync(string title, short year, string mediumType, string location, short length, string route)
        {
            
            var mediumToPost = new BaseMediumDto(title, year, mediumType, location);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            return await GetClient().PostAsync(route, jsonContent);
        }
        protected async Task<HttpResponseMessage> PutMediumAsync(string title, short year, string mediumType, string route)
        {
            var mediumToPut = new BaseMediumDto(title, year, mediumType);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPut), Encoding.UTF8, "application/json");
            return await GetClient().PutAsync(route, jsonContent);
        }
        protected async Task<HttpResponseMessage> DeleteMediumAsync(string title, short year, string mediumType, string route)
        {
            var key = _keyService.ConstructMediumKey(title, year, mediumType);
            return await GetClient().DeleteAsync($"{route}/{key}");
        }
        // Access vertical slice Person
        protected async Task<List<Person>> GetPersonAsync(string route)
        {
            var result = new List<Person>();
            var response = await GetClient().GetAsync(route);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<KeyedPersonDto>>(stringResponse);
            foreach (var k in list)
            {
                var p = (KeyedPersonDto)k;
                result.Add(new Person(p.LastName, p.Birthdate, p.FirstMidName));
            }
            return result;
        }
        protected async Task<HttpResponseMessage> GetPersonAsync(string lastName, string birthdate, string route)
        {
            var key = _keyService.ConstructPersonKey(lastName, birthdate);
            return await GetPersonWithKeyAsync(key, route);
        }
        protected async Task<HttpResponseMessage> GetPersonWithKeyAsync(string key, string route)
        {
            return await GetClient().GetAsync($"{route}/{key}");
        }
        protected async Task<Person> CompleteGetPersonAsync(string lastName,string birthdate, string route)
        {
            var response = await GetPersonAsync(lastName, birthdate, route);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var p = JsonConvert.DeserializeObject<KeyedPersonDto>(stringResponse);
            return new Person(p.LastName, p.Birthdate, p.FirstMidName);
        }
        protected async Task<HttpResponseMessage> PostPersonAsync(string lastName, string birthdate, string firstMidName, string route)
        {
            var personToPost = new BasePersonDto(lastName, birthdate, firstMidName);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToPost), Encoding.UTF8, "application/json");
            return await GetClient().PostAsync(route, jsonContent);
        }
        protected async Task<Person> CompletePostPersonAsync(string lastName, string birthdate, string firstMidName, string route)
        {
            var response = await PostPersonAsync(lastName, birthdate, firstMidName, route);
            //response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var k = JsonConvert.DeserializeObject<KeyedPersonDto>(stringResponse);
            return new Person(k.LastName, k.Birthdate, k.FirstMidName);
        }
        protected async Task<HttpResponseMessage> PutPersonAsync(string lastName, string birthdate, string route)
        {
            var personToUpdate = new BasePersonDto(lastName, birthdate);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToUpdate), Encoding.UTF8, "application/json");
            return await GetClient().PutAsync(route, jsonContent); 
        }
        protected async Task<HttpResponseMessage> DeletePersonAsync(string lastName, string birthdate, string route)
        {
            var key = _keyService.ConstructPersonKey(lastName, birthdate);
            return await GetClient().DeleteAsync($"{route}/{key}");
        }
    }
}
