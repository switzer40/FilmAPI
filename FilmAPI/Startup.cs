using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Constants;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Infrastructure.Data;
using FilmAPI.Infrastructure.Repositories;
using FilmAPI.Interfaces;
using FilmAPI.Mappers;
using FilmAPI.Services;
using FilmAPI.Validation.Interfaces;
using FilmAPI.Validation.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FilmAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {            
            services.AddDbContext<FilmContext>(Options =>
            Options.UseInMemoryDatabase(System.Guid.NewGuid().ToString()));
            ConfigureServices(services);
        }
        public void ConfigureTestingServices(IServiceCollection services)
        {
            services.AddDbContext<FilmContext>(Options =>
            Options.UseInMemoryDatabase("TestDb"));
            ConfigureServices(services);
        }
        private void ConfigureServices(IServiceCollection services)
        {
            // Add system services
            services.AddMvc();

            // Register  application services
            services.AddTransient<IFilmService, FilmService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IMediumService, MediumService>();
            services.AddTransient<IFilmPersonService, FilmPersonService>();

            // Register mappers
            services.AddTransient<IFilmMapper, FilmMapper>();
            services.AddTransient<IFilmPersonMapper, FilmPersonMapper>();
            services.AddTransient<IMediumMapper, MediumMapper>();
            services.AddTransient<IPersonMapper, PersonMapper>();

            // Register repositories
            services.AddTransient<IFilmRepository, FilmRepository>();
            services.AddTransient<IFilmPersonRepository, FilmPersonRepository>();
            services.AddTransient<IMediumRepository, MediumRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();

            // Register validators
            services.AddTransient<IFilmValidator, FilmValidator>();
            services.AddTransient<IFilmPersonValidator, FilmPersonValidator>();
            services.AddTransient<IMediumValidator, MediumValidator>();
            services.AddTransient<IPersonValidator, PersonValidator>();

            // Register error service
            services.AddTransient<IErrorService, ErrorService>();
            services.AddTransient(typeof(OperationStatus));                        
        }

        
        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<FilmContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            ConfigureServices(services);
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, FilmContext context)
        {
            if (!env.IsProduction())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMvc();
            PopulateData(context);
        }

        private void PopulateData(FilmContext context)
        {
            if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                // If the DB exists do nothing
                return;
            }
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated(); // This will be mistake, if I later need migrations.
            var tiffany = AddAFilm(context, "Frühstück bei Tiffany", (short)1961, (short)110);
            var pretty =  AddAFilm(context, "Pretty Woman", 1990, 109);
            var hepburn = AddAPerson(context, "Audrey", "Hepburn", "1929-05-04");
            var roberts = AddAPerson(context, "Julia", "Roberts", "1967-10-28");
            var tiffanyDVD = AddAMedium(context, tiffany, FilmConstants.MediumType_DVD, FilmConstants.Location_Left);
            var prettyDVD = AddAMedium(context, pretty, FilmConstants.MediumType_DVD, FilmConstants.Location_Left);
            AddAFilmPerson(context, tiffany, hepburn, FilmConstants.Role_Actor);
            AddAFilmPerson(context, pretty, roberts, FilmConstants.Role_Actor);
        }

        private void AddAFilmPerson(FilmContext context, int filmId, int personId, string role)
        {
            var fp = new FilmPerson(filmId, personId, role);
            context.FilmPeople.Add(fp);
            context.SaveChanges();
        }

        private int AddAMedium(FilmContext context, int filmId, string mediumType, string location)
        {
            var m = new Medium(filmId, mediumType, location, true);
            context.Media.Add(m);
            context.SaveChanges();
            return m.Id;
        }

        private int AddAPerson(FilmContext context, string firstMidName, string lastName, string birthdate)
        {
            var p = new Person(lastName, birthdate, firstMidName);
            context.People.Add(p);
            context.SaveChanges();
            return p.Id;
        }

        private int AddAFilm(FilmContext context, string title, short year, short length)
        {
            var f = new Film(title, year, length);
            context.Films.Add(f);
            context.SaveChanges();
            return f.Id;
        }
    }
}
