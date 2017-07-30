using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces
{
    public interface IKeyService
    {
        // Properties
        string FilmTitle { get; set; }
        short FilmYear { get; set; }

        string PersonLastName { get; set; }
        string PersonBirthdate { get; set; }
        int MediumFilmId { get; set; }
        string MediumType { get; set; }
        int FilmPersonFilmId { get; set; }
        int FilmPersonPersonId { get; set; }
        string FilmPersonRole { get; set; }

        //Methods
        string ConstructFilmSurrogateKey(string title, short year);
        string ConstructPersonSurrogateKey(string lastName, string birthdate);
        string ConstructMediumSurrogateKey(int filmId, string mediumType);
        string ConstructFilmPersonSurrogateKey(int filmId, int personId, string role);
        void DeconstructFilmSurrogateKey(string key);
        void DeconstructPersonSurrogateKey(string key);
        void DeconstructzMediumSurrogateKey(string key);
        void DeconstructFilmPPersonSurrogateKey(string key);
    }
}
