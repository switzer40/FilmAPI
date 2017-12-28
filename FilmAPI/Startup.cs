using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FilmAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FilmAPI.Core.Entities;
using FilmAPI.Core.SharedKernel;
using StructureMap;
using FilmAPI.Common.Services;
using FilmAPI.Mappers;
using Swashbuckle.AspNetCore.Swagger;
using FilmAPI.Interfaces.Mappers;
using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FluentValidation.AspNetCore;
using FluentValidation;
using FilmAPI.Common.Validators;

namespace FilmAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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

        public IServiceProvider ConfigureDevelopmentServices(IServiceCollection services){
            services.AddDbContext<FilmContext>(options =>
            {
                options.UseInMemoryDatabase();
            });


            //services.UseSqlServer();
            return ConfigureServices(services);


        }
        public IServiceProvider ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<FilmContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            return ConfigureServices(services);
        }

        public IServiceProvider ConfigureTestingServices(IServiceCollection services)
        {
            services.AddDbContext<FilmContext>(options =>
            {
                var dbName = Guid.NewGuid().ToString();
                options.UseInMemoryDatabase(dbName);
            });
            return ConfigureServices(services);
        }

        // This m ethod gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<BaseFilmDto>, FilmValidator>();
            services.AddTransient<IValidator<BaseFilmPersonDto>, FilmPersonValidator>();
            services.AddTransient<IValidator<BaseMediumDto>, MediumValidator>();
            services.AddTransient<IValidator<BasePersonDto>, PersonValidator>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "FilmAPI", Version = "v1" });
            });                         
            return ConfigureIoC(services);
        }

        private IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var container = new Container();
            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Startup));
                    _.AssemblyContainingType(typeof(KeyService));
                    _.AssemblyContainingType(typeof(Film));
                    _.AssemblyContainingType(typeof(FilmContext));               
                    _.WithDefaultConventions();

                });
                config.For(typeof(IFilmPersonMapper)).Add(typeof(FilmPersonMapper));
                //config.For(typeof(IFilmPersonService)).Add(typeof(FilmPersonService));
                config.For(typeof(IKeyedDto<Film>)).Add(typeof(KeyedFilmDto));
                config.For(typeof(IKeyedDto<Person>)).Add(typeof(KeyedPersonDto));
                config.For(typeof(IKeyedDto<Medium>)).Add(typeof(KeyedMediumDto));
                config.For(typeof(IKeyedDto<FilmPerson>)).Add(typeof(KeyedFilmPersonDto));
                config.Populate(services);
            });
            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, FilmContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
             app.UseDeveloperExceptionPage();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FilmAPI V1");
            });
            app.UseMvc();
            PopulateData(context);
        }

        private void PopulateData(FilmContext context)
        {
            RemoveAll(context);
            var tiffany = new Film("Frühstück bei Tiffany", 1961, 110);
            context.Films.Add(tiffany);
            context.SaveChanges();
            var woman = new Film("Pretty Woman", 1990, 109);
            context.Films.Add(woman);
            context.SaveChanges();
            var hepburn = new Person("Hepburn", "1929-05-04", "Audrey");
            context.People.Add(hepburn);
            context.SaveChanges();
            var roberts = new Person("Roberts", "1967-10-28", "Julia");
            context.People.Add(roberts);
            context.SaveChanges();
            var gere = new Person("Gere", "1949-08-31", "Richard");
            context.People.Add(gere);
            context.SaveChanges();
            var tiffanyHepburn = new FilmPerson(tiffany.Id, hepburn.Id, FilmConstants.Role_Actor);
            context.FilmPeople.Add(tiffanyHepburn);
            context.SaveChanges();
            var womanRoberts = new FilmPerson(woman.Id, roberts.Id, FilmConstants.Role_Actor);
            context.FilmPeople.Add(womanRoberts);
            context.SaveChanges();
            var tiffanyDVD  = new Medium(tiffany.Id, FilmConstants.MediumType_DVD, FilmConstants.Location_Left);
            context.Media.Add(tiffanyDVD);
            context.SaveChanges();
            var womanDVD = new Medium(woman.Id, FilmConstants.MediumType_DVD, FilmConstants.Location_Left);
            context.Media.Add(womanDVD);
            context.SaveChanges();

        }

        private void RemoveAll(FilmContext context)
        {
            foreach (var f in context.Films)
            {
                context.Films.Remove(f);
            }
            foreach (var p in context.People)
            {
                context.People.Remove(p);
            }
            foreach (var m in context.Media)
            {
                context.Media.Remove(m);
            }
            foreach (var fp in context.FilmPeople)
            {
                context.FilmPeople.Remove(fp);
            }
            context.SaveChanges();
        }
    }
}
