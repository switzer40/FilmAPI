using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.VviewModls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public class FilmPersonConverter
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        public FilmPersonConverter(IFilmRepository frepo, IPersonRepository prepo)
        {
            _filmRepository = frepo;
            _personRepository = prepo;
        }
        public FilmPerson Convert(FilmPersonViewModel model)
        {
            Film f = _filmRepository.GetByTitleAndYear(m.FilmTitle, m.FilmYear);
            Person p = _personRepository.GetByLastNameAndBirthdate(m.PersonLastName, m.PersonBirthdate);
            return new FilmPerson(f.Id, p.Id, model.Role);
        }
    }
}
