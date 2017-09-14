using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Interfaces;
using FilmAPI.Core.SharedKernel;

namespace FilmAPI.Services
{
    public class KeyService : IKeyService
    {
        private const char SEPCHAR = '*';
        private char[] _separators = {SEPCHAR};
        private const string BADKEY = FilmConstants.BADKEY;
        private const string FORTYTWO = FilmConstants.FORTYTWO;
        
        public string ConstructFilmPersonSurrogateKey(string title, short year, string lastName, string birthdate, string role)
        {
            return $"{title}{SEPCHAR}{year}{SEPCHAR}{lastName}{SEPCHAR}{birthdate}{SEPCHAR}{role}";
        }

        public string ConstructFilmSurrogateKey(string title, short year)
        {
            return $"{title}{SEPCHAR}{year}";
        }

        public string ConstructMediumSurrogateKey(string title, short year, string mediumType)
        {
            return $"{title}{SEPCHAR}{year}{SEPCHAR}{mediumType}";
        }

        public string ConstructPersonSurrogateKey(string lastName, string birtdate)
        {
            return $"{lastName}{SEPCHAR}{birtdate}";
        }

        public (string, short, string, string, string) DeconstructFilmPersonSurrogateKey(string key)
        {
            string[] parts = ParseKey(key, 5);
            if (parts[0] == BADKEY)
            {
                parts[1] = FORTYTWO;
                parts[2] = BADKEY;
                parts[3] = BADKEY;
                parts[4] = BADKEY;
            }
            return (parts[0], short.Parse(parts[1]), parts[2], parts[3], parts[4]);
        }

        public (string, short) DeconstructFilmSurrogateKey(string key)
        {
            string[] parts = ParseKey(key, 2);
            if (parts[0] == BADKEY)
            {
                parts[1] = FORTYTWO;
            }
            return (parts[0], short.Parse(parts[1]));
        }

        public (string, short, string) DeconstructMediumSurrogateKey(string key)
        {
            string[] parts = ParseKey(key, 3);
            if (parts[0] == BADKEY)
            {
                parts[1] = FORTYTWO;
                parts[2] = BADKEY;
                
            }
            return (parts[0], short.Parse(parts[1]), parts[2]);
        }

        public (string, string) DeconstructPesonSurrogateKey(string key)
        {
            string[] parts = ParseKey(key, 2);
            if (parts[0] == BADKEY)
            {
                parts[1] = BADKEY;
            }                    
            return (parts[0], parts[1]);
        }
        private string[] ParseKey(string key, int expectedLength)
        {
            string[] result = new string[expectedLength];
            if (string.IsNullOrEmpty(key))
            {
                result[0] = BADKEY;
            }
            result = key.Split(_separators);
            if (result.Count() != expectedLength)
            {
                result = new string[expectedLength];
                result[0] = BADKEY;
            }
            return result;
        }
    }
}
