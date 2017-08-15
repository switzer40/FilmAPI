using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FilmAPI.Interfaces;
using FilmAPI.Services;
using FilmAPI.Core.Interfaces;
using FilmAPI.Infrastructure.Repositories;
using FilmAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FilmAPI.Core.Entities;
using AutoMapper;

namespace FilmAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public void ConfigureTestingServices(IServiceCollection services)
        {                       
            services.AddDbContext<FilmContext>(options =>
            {
                options.UseInMemoryDatabase();
            });
            ConfigureServices(services);
        }

        private FilmContext BuildAndPopulateContext(DbContextOptionsBuilder<FilmContext> builder, bool refresh)
        {
            FilmContext context = new FilmContext(builder.Options);
            if (refresh)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Films.AddRange(BuildSomeFilms());
                context.SaveChanges();
            }            
            return context;
        }

        private Film[] BuildSomeFilms()
        {
            Film[] result =
            {
                new Film("Frühstück bei Tiffany", 1961, 110),
                new Film("Pretty Woman", 1990, 109)                
            };
            return result;
        }
        private Person[] BuildSomePeople()
        {
            Person[] result =
            {
                new Person("Hepburn", "1929-05-04", "Audrey"),
                new Person("Roberts", "1967-10-28", "Julia")

            };
            return result;
        }

        public void ConfigureDevelopmentServices(IServiceCollection services){
            services.AddDbContext<FilmContext>(options =>
            {
                options.UseInMemoryDatabase();
            });


            //services.UseSqlServer();
            ConfigureServices(services);


        }
        // This m ethod gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
        
            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            //services.AddScoped<IMediumRepository, MediumRepository>();
            //services.AddScoped<IFilmPersonRepository, FilmPersonRepository>();
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IFilmPersonService, FilmPersonService>();
            services.AddScoped<IMediumService, MediumService>();
            services.AddScoped<IKeyService, KeyService>();
            services.AddMvc();
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperConfig()); });
            var mapper = config.CreateMapper();
            services.AddScoped<MapperConfiguration>(_ => config);
            services.AddScoped<IMapper>(_ => mapper);

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, FilmContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            PopulateData(context);
        }

        private void PopulateData(FilmContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Films.AddRange(BuildSomeFilms());
            context.People.AddRange(BuildSomePeople());
            context.SaveChanges();

        }
    }
}
