using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Mappers
{
    public class FilmPersonMapper : EntityMapper<FilmPerson, FilmPersonViewModel>, IFilmPersonMapper
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IKeyService _keyService;
        public FilmPersonMapper(IFilmRepository frepo, IPersonRepository prepo, IKeyService service)
        {
            _filmRepository = frepo;
            _personRepository = prepo;
            _keyService = service;
        }
        public override FilmPersonViewModel Map(FilmPerson e)
        {
            Film f = _filmRepository.GetById(e.FilmId);
            Person p = _personRepository.GetById(e.PersonId);
            string key = _keyService.ConstructFilmPersonSurrogateKey(f.Title, f.Year, p.LastName, p.BirthdateString, e.Role);
            return new FilmPersonViewModel(f, p, e.Role, key);
        }

        public override FilmPerson MapBack(FilmPersonViewModel m)
        {
            Film f = new Film(m.FilmTitle, m.FilmYear);
            Person p = _personRepository.GetByLastNameAndBirthdate(m.PersonLastName, m.PersonBirthdate);
            return new FilmPerson(f.Id, p.Id, m.Role);
        }
    }
}
