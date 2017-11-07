using FilmAPI.Common.DTOs.FilmPerson;
using FilmAPI.Common.DTOs.Medium;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Exceptions;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces.FilmPerson;
using FilmAPI.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public class FilmPersonService : EntityService<FilmPerson, BaseFilmPersonDto, KeyedFilmPersonDto>, IFilmPersonService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        public FilmPersonService(IFilmPersonRepository repo,
                                 IFilmPersonMapper mapper,
                                 IFilmRepository frepo,
                                 IPersonRepository prepo) : base(repo, (BaseMapper<FilmPerson, BaseFilmPersonDto>)mapper)
        {
            _filmRepository = frepo;
            _personRepository = prepo;
        }

        protected override KeyedFilmPersonDto GenerateOutType(FilmPerson fp)
        {
            Film f = _filmRepository.GetById(fp.FilmId);
            Person p = _personRepository.GetById(fp.PersonId);
            if (f == null)
            {
                throw new UnknownFilmException("Rocky Horror Picture Show", 1900);
            }
            if (p == null)
            {
                throw new UnknownPersonException("Walkes", "1900-07-32");
            }
            var key = _keyService.ConstructFilmPersonKey(f.Title, f.Year, p.LastName, p.BirthdateString, fp.Role);
            return new KeyedFilmPersonDto(f.Title, f.Year, p.LastName, p.BirthdateString, fp.Role, f.Length, p.FirstMidName, key);
        }
    }
}
