using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.DTOs;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces.Mappers;

namespace FilmAPI.Services
{
    public class FilmPersonService : BaseSevice<FilmPerson>, IFilmPersonService
    {
        public FilmPersonService(IFilmPersonRepository repo,
                                 IFilmPersonMapper mapper) : base(repo, mapper)
        {
        }

        protected override IKeyedDto<FilmPerson> ExtractKeyedDto(IBaseDto<FilmPerson> dto)
        {
            var b = (BaseFilmPersonDto)dto;
            var key = _keyService.ConstructFilmPersonKey(b.Title, b.Year, b.LastName, b.Birthdate, b.Role);
            var result = new KeyedFilmPersonDto(b.Title, b.Year, b.LastName, b.Birthdate, b.Role, key);
            return (IKeyedDto<FilmPerson>)result;
        }

        protected override FilmPerson RetrieveStoredEntity(IBaseDto<FilmPerson> dto)
        {
            var b = (BaseFilmPersonDto)dto;
            return ((IFilmPersonRepository)_repository).GetByTitleYearLastNameBirtdateAndRole(b.Title, b.Year, b.LastName, b.Birthdate, b.Role);
        }
    }
}
