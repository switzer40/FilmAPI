using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Interfaces;

namespace FilmAPI.Services
{
    public class KeyService : IKeyService
    {
        private const char SEPCHAR = '*';
        private char[] _separators = {SEPCHAR};
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
            string[] parts = key.Split(_separators);
            return (parts[0], short.Parse(parts[1]), parts[2], parts[3], parts[4]);
        }

        public (string, short) DeconstructFilmSurrogateKey(string key)
        {
            string[] parts = key.Split(_separators);
            return (parts[0], short.Parse(parts[1]));
        }

        public (string, short, string) DeconstructMediumSurrogateKey(string key)
        {
            string[] parts = key.Split(_separators);
            return (parts[0], short.Parse(parts[1]), parts[2]);
        }

        public (string, string) DeconstructPesonSurrogateKey(string key)
        {
            if (key == null)
            {
                throw new BadKeyException("null");
            }
            string[] parts = key.Split(_separators);
            return (parts[0], parts[1]);
        }
    }
}
