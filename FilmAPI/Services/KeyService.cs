using FilmAPI.Interfaces;
using FilmAPI.ViewModels;

namespace FilmAPI.Services
{
    public class KeyService : IKeyService
    {
        private const char SEPCHAR = '*';
        private char[] separators = {  SEPCHAR  };
        // Properties
        private string filmTitle;

        public string FilmTitle
        {
            get { return filmTitle; }
            set { filmTitle = value; }
        }

        private short filmYear;

        public short FilmYear
        {
            get { return filmYear; }
            set { filmYear = value; }
        }

        private string personLastName;

        public string PersonLastName
        {
            get { return personLastName; }
            set { personLastName = value; }
        }

        private string personBirthdate;

        public string PersonBirthdate
        {
            get { return personBirthdate; }
            set { personBirthdate = value; }
        }

        private int filmPersonFilmId;

        public int FilmPersonFilmId
        {
            get { return filmPersonFilmId; }
            set { filmPersonFilmId = value; }
        }

        private int filmPersonPersonId;

        public int FilmPersonPersonId
        {
            get { return filmPersonPersonId; }
            set { filmPersonPersonId = value; }
        }

        private string filmPersonRole;

        public string FilmPersonRole
        {
            get { return filmPersonRole; }
            set { filmPersonRole = value; }
        }

        private int mediumFilmId;

        public int MediumFilmId
        {
            get { return mediumFilmId; }
            set { mediumFilmId = value; }
        }

        private string mediumMediumType;

        public string MediumMediumType
        {
            get { return mediumMediumType; }
            set { mediumMediumType = value; }
        }


        // Methods
        string IKeyService.ConstructFilmPersonSurrorgateKey(FilmPersonViewModel model)
        {
            return ConstructFilmPersonSurrogateKey(model.FilmTitle, model.FilmYear, model.PersonLastName, model.PersonBirthdate, model.Role);
        }

        string IKeyService.ConstructFilmSurrogateKey(FilmViewModel model)
        {
            return ConstructFilmSurrogateKey(model.Title, model.Year);
        }

        string IKeyService.ConstructMediumSurrogateKey(MediumViewModel model)
        {
            return ConstructMediumSurrogateKey(model.FilmTitle, model.FilmYear, model.MediumType);
        }

        string IKeyService.ConstructPersonSurrogateKey(PersonViewModel model)
        {
            return ConstructPersonSurrogateKey(model.LastName, model.BirthdateString);
        }

        void IKeyService.DeconstructFilmPersonSurrogateKey(string key)
        {
            
            string[] parts = key.Split(separators);
            FilmPersonFilmId = int.Parse(parts[0]);
            FilmPersonPersonId = int.Parse(parts[1]);
            FilmPersonRole = parts[2];

        }

        void IKeyService.DeconstructFilmSurrogateKey(string key)
        {           
            string[] parts = key.Split(separators);
            FilmTitle = parts[0];
            FilmYear = short.Parse(parts[1]);
        }

        void IKeyService.DeconstructMedumSurrogateKey(string key)
        {           
            string[] parts = key.Split(separators);
            MediumFilmId  =int.Parse(parts[0]);
            MediumMediumType = parts[1];
        }

        void IKeyService.DeconstructPesonSurrogateKey(string key)
        {           
            string[] parts = key.Split(separators);
            PersonLastName = parts[0];
            PersonBirthdate = parts[1];
        }

        public string ConstructFilmSurrogateKey(string title, short year)
        {
            return $"{title}{SEPCHAR}{year}";
        }

        public string ConstructPersonSurrogateKey(string lastName, string birtdate)
        {
            return $"{lastName}{SEPCHAR}{birtdate}";
        }

        public string ConstructFilmPersonSurrogateKey(string title, short year, string lastName, string birthdate, string role)
        {
            return $"{title}{SEPCHAR}{year}{SEPCHAR}{lastName}{SEPCHAR}{birthdate}{SEPCHAR}{role}";
        }

        public string ConstructMediumSurrogateKey(string title, short year, string mediumType)
        {
            return $"{title}{SEPCHAR}{year}{SEPCHAR}{mediumType}";
        }
    }
}
