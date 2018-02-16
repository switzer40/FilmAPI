using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.Specifications;
using FilmAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmPersonRepository : Repository<FilmPerson>, IFilmPersonRepository
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        public FilmPersonRepository(FilmContext context,
                                    IFilmRepository frepo,
                                    IPersonRepository prepo) : base(context)
        {
            _filmRepository = frepo;
            _personRepository = prepo;
        }

        public override OperationStatus Delete(string key)
        {
            var res = GetByKey(key);
            if (res.status != OperationStatus.OK)
            {
                return res.status;
            }
            return Delete(res.value);
        }

        public (OperationStatus status, FilmPerson value) GetByFilmIdPersonIdAndRole(int filmId, int personId, string role)
        {
            ISpecification<FilmPerson> spec = new FilmPersonByFilmIdPersonIdAndRole(filmId, personId, role);
            var data = List(spec);
            var val = new FilmPerson();
            var status = data.status;
            if (status == OperationStatus.OK)
            {
                val = data.value.SingleOrDefault();
            }
            else
            {
                val = null;
            }
            return (status, val);
        }

        public (OperationStatus status, FilmPerson value) GetByKey(string key)
        {
            var data = _keyService.DeconstructFilmPersonKey(key);
            return GetByTitleYearLastNameBirthdateAndRole(data.title, data.year, data.lastName, data.birthdate, data.role);
        }

        public (OperationStatus status, FilmPerson value) GetByTitleYearLastNameBirthdateAndRole(string title, short year, string lastName, string birthdate, string role)
        {
            var fdata = _filmRepository.GetByTitleAndYear(title, year);
            var pdata = _personRepository.GetByLastNameAndBirthdate(lastName, birthdate);
            Film f = null;
            Person p = null;
            FilmPerson fp = null;
            var status = fdata.status;
            if (status == OperationStatus.OK)
            {
                f = fdata.value;
                status = pdata.status;
            }
            if (status ==OperationStatus.OK && f != null)
            {
                p = pdata.value;
            }
            if (f != null && p != null)
            {
                fp = new FilmPerson(f.Id, p.Id, role);
            }
            return (status, fp);
        }
    }
}
