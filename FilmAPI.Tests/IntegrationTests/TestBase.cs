using FilmAPI.DTOs.Film;
using FilmAPI.DTOs.FilmPerson;
using FilmAPI.DTOs.Medium;
using FilmAPI.DTOs.Person;
using FilmAPI.Interfaces;
using FilmAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
        protected async Task<HttpResponseMessage> GetFilmAsync(string title, short year, string route)
        {
            var key = _keyService.ConstructFilmSurrogateKey(title, year);
            return await GetClient().GetAsync($"{route}/{key}");
        }
        protected async Task<HttpResponseMessage> PostFilmAsync(string title, short year, short length, string route)
        {
            var filmToPost = new BaseFilmDto(title, year, length);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPost), Encoding.UTF8, "application/json");
            return await GetClient().PostAsync(route, jsonContent);
        }
        protected async Task<HttpResponseMessage> PutFilmAsync(string title, short year, short length, string route)
        {
            var filmToPut = new BaseFilmDto(title, year, length);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPut), Encoding.UTF8, "application/json");
            return await GetClient().PutAsync(route, jsonContent);
        }
        protected async Task<HttpResponseMessage> DeleteFilmAsync(string title, short year, string route)
        {
            var key = _keyService.ConstructFilmSurrogateKey(title, year);
            return await GetClient().DeleteAsync($"{route}/{key}");
        }
        // Access vertical slice FilmPerso
        protected async Task<HttpResponseMessage> GetFilmPersonAsync(string title, short year, string lastName, string birthdate, string role, string route)
        {
            var key = _keyService.ConstructFilmPersonSurrogateKey(title, year, lastName, birthdate, role);
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
            var key = _keyService.ConstructFilmPersonSurrogateKey(title, year, lastName, birthdate, role);
            return await GetClient().DeleteAsync($"{route}/{key}");
        }
        // Access vertical slice Medium
        protected async Task<HttpResponseMessage> GetMediumAsync(string title, short year, string mediumType, string route)
        {
            var key = _keyService.ConstructMediumSurrogateKey(title, year, mediumType);
            return await GetClient().GetAsync($"{route}/{key}");
        }
        protected async Task<HttpResponseMessage> PostMediumAsync(string title, short year, string mediumType, string location, short length, string route)
        {
            
            var mediumToPost = new BaseMediumDto(title, year, mediumType, location, length);
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
            var key = _keyService.ConstructMediumSurrogateKey(title, year, mediumType);
            return await GetClient().DeleteAsync($"{route}/{key}");
        }
        // Access vertical slice Person
        protected async Task<HttpResponseMessage> GetPersonAsync(string lastName, string birthdate, string route)
        {
            var key = _keyService.ConstructPersonSurrogateKey(lastName, birthdate);
            return await GetClient().GetAsync($"{route}/{key}");
        }
        protected async Task<HttpResponseMessage> PostPersonAsync(string lastName, string birthdate, string firstMidName, string route)
        {
            var personToPost = new BasePersonDto(lastName, birthdate, firstMidName);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToPost), Encoding.UTF8, "application/json");
            return await GetClient().PostAsync(route, jsonContent);
        }
        protected async Task<HttpResponseMessage> PutPersonAsync(string lastName, string birthdate, string route)
        {
            var personToUpdate = new BasePersonDto(lastName, birthdate);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToUpdate), Encoding.UTF8, "application/json");
            return await GetClient().PutAsync(route, jsonContent); 
        }
        protected async Task<HttpResponseMessage> DeletePersonAsync(string lastName, string birthdate, string route)
        {
            var key = _keyService.ConstructPersonSurrogateKey(lastName, birthdate);
            return await GetClient().DeleteAsync($"{route}/{key}");
        }
    }
}
