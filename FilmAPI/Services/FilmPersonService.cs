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
using FilmAPI.Common.Constants;

namespace FilmAPI.Services
{
    public class FilmPersonService : BaseSevice<FilmPerson>, IFilmPersonService
    {
        public FilmPersonService(IFilmPersonRepository repo,
                                 IFilmPersonMapper mapper) : base(repo, mapper)
        {
        }

        public override OperationStatus Add(IBaseDto<FilmPerson> b)
        {
            throw new NotImplementedException();
        }

        public override OperationStatus Delete(string key)
        {
            var result = OperationStatus.OK;
            (string title,
             short year,
             string lastName,
             string birthdate,
             string role) data = _keyService.DeconstructFilmPersonKey(key);
            if (data.title == FilmConstants.BADKEY)
            {
                result = OperationStatus.BadRequest;
            }
            var filmPersonToDelete = ((IFilmPersonRepository)_repository).GetByTitleYearLastNameBirtdateAndRole(data.title,
                                                                                                                data.year,
                                                                                                                data.lastName,
                                                                                                                data.birthdate,
                                                                                                                data.role);
            if (filmPersonToDelete == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Delete(filmPersonToDelete);
            }
            return result;
        }

        public override OperationStatus Update(IBaseDto<FilmPerson> dto)
        {
            var result = OperationStatus.OK;
            var b = (BaseFilmPersonDto)dto;
            if (b == null)
            {
                result = OperationStatus.BadRequest;
            }
            var filmPersonToUpdate = _mapper.MapBack(b);
            var storedFilmPerson = ((IFilmPersonRepository)_repository).GetByTitleYearLastNameBirtdateAndRole(b.Title,
                                                                                                               b.Year,
                                                                                                               b.LastName,
                                                                                                               b.Birthdate,
                                                                                                               b.Role);
            if (storedFilmPerson == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Update(filmPersonToUpdate);
            }
            return result;
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
