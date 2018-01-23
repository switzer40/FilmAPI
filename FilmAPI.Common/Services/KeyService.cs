using FilmAPI.Common.Constants;
using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Services
{
    public class KeyService : IKeyService
    {
        private static char SEPCHAR = '*';
        private char[] separators = { SEPCHAR };
        public string ConstructFilmKey(string title, short year)
        {
            return $"{title}{SEPCHAR}{year}";
        }

        public string ConstructFilmPersonKey(string title, short year, string lastName, string birthdate, string role)
        {
            return $"{title}{SEPCHAR}{year}{SEPCHAR}{lastName}{SEPCHAR}{birthdate}{SEPCHAR}{role}";
        }

        public string ConstructMediumKey(string title, short year, string mediumType)
        {
            return $"{title}{SEPCHAR}{year}{SEPCHAR}{mediumType}";
        }

        public string ConstructPersonKey(string lastName, string birthdate)
        {
            return $"{lastName}{SEPCHAR}{birthdate}";
        }

        public (string title, short year) DeconstructFilmKey(string key)
        {
            string[] parts = ParseKey(key, 2);
            var title = parts[0];
            var year = (short)1970;
            if (title != FilmConstants.BADKEY)
            {
                year = short.Parse(parts[1]);
            }
            
            return (title, year);
        }

        private string[] ParseKey(string key, int expectedCount)
        {
            string[] result = key.Split(separators);
            if (result.Length != expectedCount)
            {
                result = new string[expectedCount];
                result[0] = FilmConstants.BADKEY;
            }
            return result;
        }

        public (string title, short year, string lastName, string birthdate, string role) DeconstructFilmPersonKey(string key)
        {
            string[] parts = ParseKey(key, 5);
            var title = parts[0];
            var year = (short)10;
            var lastName = "Kilroy";
            var birthdate = "1900-01-01";
            var role = FilmConstants.Role_Actor;
            if (title != FilmConstants.BADKEY)
            {
                year = short.Parse(parts[1]);
                lastName = parts[2];
                birthdate = parts[3];
                role = parts[4];
            }
            
            return (title, year, lastName, birthdate, role);
        }

        public (string title, short year, string mediumType) DeconstructMediumKey(string key)
        {
            string[] parts = ParseKey(key, 3);
            var title = parts[0];
            var year = (short)10;
            var mediumType = FilmConstants.MediumType_DVD;
            if (title != FilmConstants.BADKEY)
            {
                year = short.Parse(parts[1]);
                mediumType = parts[2];
            }
            
            return (title, year, mediumType);
        }

        public (string lastName, string birthdate) DeconstructPersonKey(string key)
        {
            string[] parts = ParseKey(key, 2);
            var lastName = parts[0];
            var birthdate = "1900-01-01";
            if (lastName != FilmConstants.BADKEY)
            {
                birthdate = parts[1];
            }
           
            return (lastName, birthdate);
        }
    }
}
