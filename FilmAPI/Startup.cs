using System;
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
using FilmAPI.Core.SharedKernel;
using StructureMap;
using FilmAPI.ViewModels;
using FilmAPI.Controllers;

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

        public IServiceProvider ConfigureDevelopmentServices(IServiceCollection services){
            services.AddDbContext<FilmContext>(options =>
            {
                options.UseInMemoryDatabase();
            });


            //services.UseSqlServer();
            return ConfigureServices(services);


        }
        // This m ethod gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddControllersAsServices();
            
            //var config = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperConfig()); });
            //var mapper = config.CreateMapper();
            //services.AddScoped<MapperConfiguration>(_ => config);
            //services.AddScoped<IMapper>(_ => mapper);
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
                _.AssemblyContainingType(typeof(IRepository<>));
                _.AssemblyContainingType(typeof(Repository<>));
                _.WithDefaultConventions();

            });
            // I hope StructureMap´s conventions will take care of configuring
            // the relationship IEntityService -> EntityService for each of the 4 entity types.
            
            //config.For(typeof(IRepository<>)).Add(typeof(Repository<>));
            //config.For(typeof(EntityService<Film, FilmViewModel>)).Add(typeof(FilmService));
            //config.For(typeof(EntityService<Person, PersonViewModel>)).Add(typeof(PersonService));
            //config.For(typeof(EntityService<Medium, MediumViewModel>)).Add(typeof(MediumService));
            //config.For(typeof(EntityService<FilmPerson, FilmPersonViewModel>)).Add(typeof(FilmPersonService));            
            //config.For(typeof(IEntityService<,>)).Add(typeof(EntityService<,>));

            // this shoIuld have been done by WithDefaultConventions:
            //config.For<IFilmPersonService>().ContainerScoped().Use<FilmPersonService>();
                config.Populate(services);
            });
            return container.GetInstance<IServiceProvider>();
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
            context.People.Add(roberts);context.SaveChanges();
            var tiffanyHepburn = new FilmPerson(tiffany.Id, hepburn.Id, FilmConstants.Role_Actor);
            context.FilmPeople.Add(tiffanyHepburn);
            context.SaveChanges();
            var womanRoberts = new FilmPerson(woman.Id, roberts.Id, FilmConstants.Role_Actor);
            context.FilmPeople.Add(womanRoberts);
            context.SaveChanges();
            var tiffanyDVD  = new Medium(tiffany.Id, FilmConstants.MediumType_DVD);
            context.Media.Add(tiffanyDVD);
            context.SaveChanges();
            var womanDVD = new Medium(woman.Id, FilmConstants.MediumType_DVD);
            context.Media.Add(womanDVD);
            context.SaveChanges();

        }
    }
}
