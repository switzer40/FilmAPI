using FilmAPI.ViewModels;
using System;

namespace FilmAPI.Interfaces
{
    public interface IKeyService
    {

        // Methods

        string ConstructFilmSurrogateKey(string title, short year);        
        string ConstructPersonSurrogateKey(string lastName, string birtdate);        
        string ConstructFilmPersonSurrogateKey(string title, short year, string lastName, string birthdate, string role);       
        string ConstructMediumSurrogateKey(string title, short year, string mediumType);
        ValueTuple<string, short> DeconstructFilmSurrogateKey(string key);        
        ValueTuple<string, short, string> DeconstructMediumSurrogateKey(string key);
        ValueTuple<string, short, string, string, string> DeconstructFilmPersonSurrogateKey(string key);
        ValueTuple<string, string> DeconstructPesonSurrogateKey(string key);
    }
}
