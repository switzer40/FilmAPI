using FilmAPI.Common.Constants;
using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Services
{
    public class KeyService: IKeyService
    {
        private const char SEPCHAR = '*';
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

        public string ConstructPersonKey(string lastNme, string birthdate)
        {
            return $"{lastNme}{SEPCHAR}{birthdate}";
        }

        public (string title, short year) DeconstructFilmKey(string key)
        {
            short year = 1800;
            string[] parts = ParseKey(key, 2);
            var title = parts[0];
            if (parts[1] != FilmConstants.BADKEY)
            {
                year = short.Parse(parts[1]);
            }            
            return (title, year);
        }

        private string[] ParseKey(string key, int expectedLength)
        {
            string[] result = new string[expectedLength];
            var parts = key.Split(separators);
            if (parts.Length == expectedLength)
            {
                result = parts;
            }
            else
            {
                for (int i = 0; i < expectedLength; i++)
                {
                    result[i] = FilmConstants.BADKEY;
                }
            }
            return result;
        }

        public (string title, short year, string lastName, string birthdate, string role) DeconstructFilmPersonKey(string key)
        {
            short year = 1800;
            string[] parts = ParseKey(key, 5);
            var title = parts[0];
            if (parts[1] != FilmConstants.BADKEY)
            {
                year = short.Parse(parts[1]);
            }            
            var lastName = parts[2];
            var birthdate = parts[3];
            var role = parts[4];
            return (title, year, lastName, birthdate, role);
        }

        public (string title, short year, string mediumType) DeconstructMediumKey(string key)
        {
            short year = 1800;
            string[] parts = ParseKey(key, 3);
            var title = parts[0];
            if (parts[1] != FilmConstants.BADKEY)
            {
                year = short.Parse(parts[1]);
            }            
            var mediumType = parts[2];
            return (title, year, mediumType);
        }

        public (string lastName, string birthdate) DeconstructPersonKey(string key)
        {
            string[] parts = ParseKey(key, 2);
            var lastName = parts[0];
            var birthdate = parts[1];
            return (lastName, birthdate);
        }
    }
}
