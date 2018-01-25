using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace FilmAPI.Tests.Integration
{
    public class TestBase
    {
        public TestBase()
        {            
            _client = GetClient();
            _route = $"http://localhost:5000/api";
            _keyService = new KeyService();
        }
        protected HttpClient _client;        
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
    }
}
