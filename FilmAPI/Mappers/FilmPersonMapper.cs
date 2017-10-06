using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.DTOs.FilmPerson;
using FilmAPI.Interfaces.FilmPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Mappers
{
    public class FilmPersonMapper : BaseMapper<FilmPerson, BaseFilmPersonDto>, IFilmPersonMapper
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        public FilmPersonMapper(IFilmRepository frepo, IPersonRepository prepo)
        {
            _filmRepository = frepo;
            _personRepository = prepo;
        }
        public override BaseFilmPersonDto Map(FilmPerson e)
        {
            var f = _filmRepository.GetById(e.FilmId);
            var p = _personRepository.GetById(e.PersonId);
            var result = new BaseFilmPersonDto(f.Title, f.Year, p.LastName, p.BirthdateString, e.Role);            
            return result;
        }

        public override FilmPerson MapBack(BaseFilmPersonDto m)
        {
            var f = _filmRepository.GetByTitleAndYear(m.Title, m.Year);
            var p = _personRepository.GetByLastNameAndBirthdate(m.LastName, m.Birthdate);
            var result = new FilmPerson(f.Id, p.Id, m.Role);
            return result;
        }
    }
}
