using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        string ConstructPersonSurrogateKey(PersonViewModel model);
        string ConstructFilmPersonSurrorgateKey(FilmPersonViewModel model);
        string ConstructMediumSurrogateKey(MediumViewModel model);
        void DeconstructFilmSurrogateKey(string key);
        void DeconstructPesonSurrogateKey(string key);
        void DeconstructFilmPersonSurrogateKey(string key);
        void DeconstructMedumSurrogateKey(string key);




    }
}
