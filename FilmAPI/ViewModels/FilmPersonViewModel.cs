using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.ViewModels
{
    public class FilmPersonViewModel : BaseViewModel
    {
        public FilmPersonViewModel(string title,
                                   short year,
                                   string lastName,
                                   string birthdate,
                                   string role,
                                   string key)
        {
            FilmTitle = title;
            FilmYear = year;
            PersonLastName = lastName;
            PersonBirthdate = birthdate;
            Role = role;
            _key = key;
        }
        public string FilmTitle { get; set; }
        public short FilmYear { get; set; }
        public string PersonLastName { get; set; }
        public string PersonBirthdate { get; set; }
        public string Role { get; set; }
        private string _key;
        public override string SurrogateKey { get { return _key; } set { _key = value; } }
    }
}
