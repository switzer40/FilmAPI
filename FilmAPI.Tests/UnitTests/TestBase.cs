using FilmAPI.Common.Constants;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Infrastructure.Data;
using FilmAPI.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Tests.UnitTests
{
    public class TestBase
    {
        protected string tiffanyTitle = "Frühstück bei Tiffany";
        protected short tiffanyYear = 1961;
        protected short tiffanyLength = 110;
        protected string womanTitle = "Pretty Woman";
        protected short womanYear = 1990;
        protected short womanLength = 109;
        protected string hepburnFirstName = "Audrey";
        protected string hepburnLastName = "Hepburn";
        protected string hepburnBirthdate = "1929-05-04";
        protected string robertsFirstName = "Julia";
        protected string robertsLastName = "Roberts";
        protected string robertsBirthdate = "1967-10-28";
        protected string actorRole = FilmConstants.Role_Actor;
        protected IFilmRepository _filmRepository;
        protected IPersonRepository _personRepository;
        protected IMediumRepository _mediumRepository;
        protected IFilmPersonRepository _filmPersonRepository;
        protected void InitializeDatabase()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<FilmContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new FilmContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new FilmContext(options))
                {
                    var tiffany =  AddFilm(context, tiffanyTitle, tiffanyYear, tiffanyLength);
                    var woman =  AddFilm(context, womanTitle, womanYear, womanLength);
                     var hepburn = AddPerson(context, hepburnFirstName, hepburnLastName, hepburnBirthdate);
                    var roberts = AddPerson(context, robertsFirstName, robertsLastName, robertsBirthdate);
                    var tiffanyDvd = AddMedium(context,
                                               tiffany,
                                               FilmConstants.MediumType_DVD,
                                               FilmConstants.Location_Left,
                                               true);
                    var womanDvd = AddMedium(context,
                                             woman,
                                             FilmConstants.MediumType_DVD,
                                             FilmConstants.Location_Left,
                                             true);
                    var tiffanyHepburn = AddFilmPerson(context,
                                                        tiffany,
                                                        hepburn,
                                                        FilmConstants.Role_Actor);
                    var womanRoberts = AddFilmPerson(context,
                                                     woman,
                                                     roberts,
                                                     FilmConstants.Role_Actor);
                    _filmRepository = new FilmRepository(context);
                    _personRepository = new PersonRepository(context);
                    _mediumRepository = new MediumRepository(context, _filmRepository);
                    _filmPersonRepository = new FilmPersonRepository(context,
                                                                     _filmRepository,
                                                                     _personRepository);
                }               
            }
            finally
            {
                connection.Close();
            }
        }

        private int AddFilmPerson(FilmContext context, int filmId, int personId, string role)
        {
            var fp = new FilmPerson(filmId, personId, role);
            context.FilmPeople.Add(fp);
            context.SaveChanges();
            return fp.Id;
        }

        private int AddMedium(FilmContext context, int filmId, string mediumType, string location, bool german)
        {
            var m = new Medium(filmId, mediumType, location, german);
            context.Media.Add(m);
            context.SaveChanges();
            return m.Id;
        }

        private int AddPerson(FilmContext context, string firstName, string lastName, string birthdate)
        {
            var p = new Person(lastName, birthdate, firstName);
            context.People.Add(p);
            context.SaveChanges();
            return p.Id;
        }

        private int AddFilm(FilmContext context, string title, short year, short length)
        {
            var f = new Film(title, year, length);
            context.Films.Add(f);
            context.SaveChanges();
            return f.Id;
        }
    }
}
