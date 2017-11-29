using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.DTOs;
using FilmAPI.Core.Interfaces;

namespace FilmAPI.Mappers
{
    public class FilmPersonMapper : BaseMapper<FilmPerson>, IFilmPersonMapper
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        public FilmPersonMapper(IFilmRepository frepo,
                                IPersonRepository prepo)
        {
            _filmRepository = frepo;
            _personRepository = prepo;
        }
        public override IBaseDto<FilmPerson> Map(FilmPerson t)
        {
            var f = _filmRepository.GetById(t.FilmId);
            var p = _personRepository.GetById(t.PersonId);
            var result = new BaseFilmPersonDto(f.Title,
                                               f.Year,
                                               p.LastName,
                                               p.BirthdateString,
                                               t.Role);
            return (IBaseDto<FilmPerson>)result;
        }

        public override FilmPerson MapBack(IBaseDto<FilmPerson> dto)
        {
            var b = (BaseFilmPersonDto)dto;
            var f = _filmRepository.GetByTitleAndYear(b.Title, b.Year);
            var p = _personRepository.GetByLastNameAndBirthdate(b.LastName, b.Birthdate);
            return new FilmPerson(f.Id, p.Id, b.Role);
        }
    }
}
