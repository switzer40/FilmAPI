using FilmAPI.Core.Entities;
using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Infrastructure.Data
{
    public class FilmInitializer
    {
        private readonly FilmContext _context;
        private static int _nextFilmKey = 1;
        private static int _nextPersonKey = 1;
        private static int _nextMediumKey = 1;
        private static int _nextFilmPersonKey = 1;
        public FilmInitializer(FilmContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            // Add two Films
            Film tiffany = AddFilm("Frühstück bei Tiffany", 1961);
            _context.Films.Add(tiffany);
            Film woman = AddFilm("Pretty Woman", 1990);
            _context.Add(woman);
            _context.SaveChanges();

            // Add two people
            Person hepburn = AddPerson("Hepburn", "1929-05-04", "Audrey");
            _context.People.Add(hepburn);
            Person roberts = AddPerson("Roberts", "1967-10-28", "Julia");
            _context.People.Add(roberts);
            _context.SaveChanges();

            // Add two media
            Medium tifDisk = AddMedium(tiffany.Id, "DVD", FilmConstants.Location_LefDrawer);
            _context.Media.Add(tifDisk);
            Medium womDisk = AddMedium(woman.Id, "DVD", FilmConstants.Location_LefDrawer);
            _context.Media.Add(womDisk);
            _context.SaveChanges();

            // Add two filmpeople
            FilmPerson tifhep = AddFilmPerson(tiffany.Id, hepburn.Id, FilmConstants.Role_Actor);
            _context.FilmPeople.Add(tifhep);
            FilmPerson womrob = AddFilmPerson(woman.Id, roberts.Id, FilmConstants.Role_Actor);
            _context.FilmPeople.Add(womrob);
            _context.SaveChanges();
        }
        public void SeedWithKeys()
        {
            // Add two Films
            Film tiffany = AddFilm("Frühstück bei Tiffany", 1961);
            tiffany.Id = _nextFilmKey++;
            _context.Films.Add(tiffany);
            Film woman = AddFilm("Pretty Woman", 1990);
            woman.Id = _nextFilmKey++;
            _context.Add(woman);
            _context.SaveChanges();

            // Add two people
            Person hepburn = AddPerson("Hepburn", "1929-05-04", "Audrey");
            hepburn.Id = _nextPersonKey++;
            _context.People.Add(hepburn);
            Person roberts = AddPerson("Roberts", "1967-10-28", "Julia");
            roberts.Id = _nextPersonKey++;
            _context.People.Add(roberts);
            _context.SaveChanges();

            // Add two media
            Medium tifDisk = AddMedium(tiffany.Id, "DVD", FilmConstants.Location_LefDrawer);
            tifDisk.Id = _nextMediumKey++;
            _context.Media.Add(tifDisk);
            Medium womDisk = AddMedium(woman.Id, "DVD", FilmConstants.Location_LefDrawer);
            womDisk.Id = _nextMediumKey++;
            _context.Media.Add(womDisk);
            _context.SaveChanges();

            // Add two filmpeople
            FilmPerson tifhep = AddFilmPerson(tiffany.Id, hepburn.Id, FilmConstants.Role_Actor);
            tifhep.Id = _nextFilmPersonKey++;
            _context.FilmPeople.Add(tifhep);
            FilmPerson womrob = AddFilmPerson(woman.Id, roberts.Id, FilmConstants.Role_Actor);
            womrob.Id = _nextFilmPersonKey++;
            _context.FilmPeople.Add(womrob);
            _context.SaveChanges();
        }

        private Film AddFilm(string title, short year)
        {
            return new Film(title, year);
        }
        private Person AddPerson(string lastName, string birthdate, string firstMidName)
        {
            return new Person(lastName, birthdate, firstMidName);
        }
        private Medium AddMedium(int filmId, string type, string location)
        {
            return new Medium(filmId, type, location);
        }
        private FilmPerson AddFilmPerson(int filmId, int personId, string role)
        {
            return new FilmPerson(filmId, personId, role);
        }
    }
}
