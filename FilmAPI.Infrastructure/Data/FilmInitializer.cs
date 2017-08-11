using FilmAPI.Core.Entities;
using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Infrastructure.Data
{
    public class FilmInitializer
    {
        
        public static void Seed(FilmContext context)
        {
            ClearFilms(context);
            Film tiffany =  AddFilm(context,  "Frühstück bei Tiffany", 1961, 110);
            Film woman = AddFilm(context, "Pretty Woman", 1990, 109);
            ClearPeople(context);
            Person hepburn = AddPerson(context, "Hepburn", "1929-05-04", "Audrey");
            Person roberts = AddPerson(context, "Roberts", "1967-10-28", "Julia");
            ClearFilmPeople(context);
            AddFilmPerson(context, tiffany.Id, hepburn.Id, FilmConstants.Role_Actor);
            AddFilmPerson(context, woman.Id, roberts.Id, FilmConstants.Role_Actor);
            ClearMedia(context);
            AddMedium(context, tiffany.Id, FilmConstants.MediumType_DVD, FilmConstants.Location_Left);
            AddMedium(context, woman.Id, FilmConstants.MediumType_DVD, FilmConstants.Location_Left);

        }

        private static Film AddFilm(FilmContext context,  string title, short year, short length)
        {
            Film f = new Film(title, year, length);
            context.Films.Add(f);
            Save(context);
            return f;
        }
        private static Person  AddPerson(FilmContext context, string lastName, string birthdate, string firstMidName)
        {
            Person p = new Person(lastName, birthdate, firstMidName);
            context.People.Add(p);
            Save(context);
            return p;
        }
        private static void AddFilmPerson(FilmContext context, int filmId, int personId, string role)
        {
            FilmPerson fp = new FilmPerson(filmId, personId, role);
            context.FilmPeople.Add(fp);
            Save(context);
        }
        private static void AddMedium(FilmContext context, int filmId, string mediumType, string location)
        {
            Medium m = new Medium(filmId, mediumType, location);
            context.Media.Add(m);
            Save(context);
        }
        private static void Save(FilmContext context)
        {
            context.SaveChanges();
        }

        private static void ClearFilms(FilmContext context)
        {
            var films = context.Films;
            foreach (var film in films)
            {
                films.Remove(film);
            }
            Save(context);
        }
        private static void ClearPeople(FilmContext context)
        {
            var people = context.People;
            foreach (var p in people)
            {
                people.Remove(p);
            }
            Save(context);
        }
        private static void  ClearFilmPeople(FilmContext context)
        {
            var filmPeople = context.FilmPeople;
            foreach (var fp in filmPeople)
            {
                filmPeople.Remove(fp);
            }
            Save(context);
        }
        private static void ClearMedia(FilmContext context)
        {
            var media = context.Media;
            foreach (var m in media)
            {
                media.Remove(m);
            }
            Save(context);
        }
    }
}
