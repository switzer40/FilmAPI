using FilmAPI.ViewModels;

namespace FilmAPI.Interfaces
{
    public interface IKeyService
    {
        // Properties
        string FilmTitle { get; set; }
        short FilmYear { get; set; }
        string PersonLastName { get; set; }
        string PersonBirthdate { get; set; }
        int FilmPersonFilmId { get; set; }
        int FilmPersonPersonId { get; set; }
        string FilmPersonRole { get; set; }

        int MediumFilmId { get; set; }
        string MediumMediumType { get; set; }

        // Methods
        string ConstructFilmSurrogateKey(FilmViewModel model);
        string ConstructFilmSurrogateKey(string title, short year);
        string ConstructPersonSurrogateKey(PersonViewModel model);
        string ConstructPersonSurrogateKey(string lastName, string birtdate);
        string ConstructFilmPersonSurrorgateKey(FilmPersonViewModel model);
        string ConstructFilmPersonSurrogateKey(string title, short year, string lastName, string birthdate, string role);
        string ConstructMediumSurrogateKey(MediumViewModel model);
        string ConstructMediumSurrogateKey(string title, short year, string mediumType);
        void DeconstructFilmSurrogateKey(string key);
        void DeconstructPesonSurrogateKey(string key);
        void DeconstructFilmPersonSurrogateKey(string key);
        void DeconstructMedumSurrogateKey(string key);        
    }
}
