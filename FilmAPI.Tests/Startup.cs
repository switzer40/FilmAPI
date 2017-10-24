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
using FilmAPI.Interfaces.FilmPerson;
using FilmAPI.Services.FilmPerson;
using FilmAPI.Common.Services;

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
            
            // temporary to run migrations
            return ConfigureProductionServices(services);

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
                options.UseInMemoryDatabase(System.Guid.NewGuid().ToString());
            });
            return ConfigureServices(services);
        }

        // This m ethod gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
                                        
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
                config.For(typeof(IFilmPersonService)).Add(typeof(FilmPesonService));
                // I had hoped StructureMap´s conventions will take care of configuring
                // the relationship I<Entity>Service -> <Entity>Service for each of the 4 entity types.
                
                //config.For(typeof(IFilmRepository)).Add(typeof(FilmRepository));
                //config.For(typeof(IPersonRepository)).Add(typeof(PersonRepository));
                //config.For(typeof(IMediumRepository)).Add(typeof(MediumRepository));
                //config.For(typeof(IFilmPersonRepository)).Add(typeof(FilmPersonRepository));
                // config.For(typeof(IFilmService)).Add(typeof(FilmService));
                //config.For(typeof(IFilmPersonService)).Add(typeof((FilmPesonService));            
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
    }
}
