using FilmAPI.Infrastructure.Data;
using FilmAPI.Interfaces;
using FilmAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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
    }
}
