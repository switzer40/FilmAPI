using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class KeyService : IKeyService
    {
        private const char _separator = '-';
        private const string _sepatorString = "-";
        private char[] _separators = { _separator };
        public string FilmTitle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public short FilmYear { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersonLastName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersonBirthdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MediumFilmId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string MediumType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int FilmPersonFilmId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int FilmPersonPersonId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FilmPersonRole { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ConstructFilmPersonSurrogateKey(int filmId, int personId, string role)
        {
            return filmId + _sepatorString + personId  +_sepatorString + role;
        }

        public string ConstructFilmSurrogateKey(string title, short year)
        {
            return title + _sepatorString + year;
        }

        public string ConstructMediumSurrogateKey(int filmId, string mediumType)
        {
            return filmId + _sepatorString + mediumType;
        }

        public string ConstructPersonSurrogateKey(string lastName, string birthdate)
        {
            return lastName + _sepatorString + birthdate;
        }

        public void DeconstructFilmPPersonSurrogateKey(string key)
        {
            string[] parts = key.Split(_separators);
            FilmPersonFilmId = int.Parse(parts[0]);
            FilmPersonPersonId = int.Parse(parts[1]);
            FilmPersonRole = parts[2];
        }

        public void DeconstructFilmSurrogateKey(string key)
        {
            string[] parts = key.Split(_separators);
            FilmTitle = parts[0];
            FilmYear = short.Parse(parts[1]);

        }

        public void DeconstructPersonSurrogateKey(string key)
        {
            string[] parts = key.Split(_separators);
            PersonLastName = parts[0];
            PersonBirthdate = parts[1];
        }

        public void DeconstructzMediumSurrogateKey(string key)
        {
            string[] parts = key.Split(_separators);
            MediumFilmId = int.Parse(parts[0]);
            MediumType = parts[1];
        }
    }
}
