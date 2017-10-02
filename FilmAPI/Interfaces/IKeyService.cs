using System;
using System.IO;

namespace FilmAPI.Interfaces
{
    public interface IKeyService
    {

        // Methods

        string ConstructFilmSurrogateKey(string title, short year);        
        string ConstructPersonSurrogateKey(string lastName, string birtdate);        
        string ConstructFilmPersonSurrogateKey(string title, short year, string lastName, string birthdate, string role);       
        string ConstructMediumSurrogateKey(string title, short year, string mediumType);
        (string title, short year) DeconstructFilmSurrogateKey(string key);        
        (string title, short year, string mediumType) DeconstructMediumSurrogateKey(string key);
        (string title, short year, string lastName, string birthdate, string role) DeconstructFilmPersonSurrogateKey(string key);
        (string lastName, string birthdate) DeconstructPesonSurrogateKey(string key);
    }
}
