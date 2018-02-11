using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Threading.Tasks;
using FilmAPI.Infrastructure.Data;
using System.Linq;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Core.Specifications;

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

        public FilmPerson GetByFilmIdPersonIdAndRole(int filmId, int personId, string role)
        {
            var spec = new FilmPersonByFilmIdPersonIdAndRole(filmId, personId, role);
            var candidates = List(spec);
            var uniqueCandidate = candidates.Single();
            return uniqueCandidate;
        }

        public async Task<FilmPerson> GetByFilmIdPersonIdAndRoleAsync(int filmId, int personId, string role)
        {
            return await Task.Run(() => GetByFilmIdPersonIdAndRole(filmId, personId, role));
        }

        public FilmPerson GetByKey(string key)
        {
            IKeyService keyService = new KeyService();
            (string title,
             short year,
             string lastName,
             string birthdate,
             string role) = keyService.DeconstructFilmPersonKey(key);
            Film f = _filmRepository.GetByTitleAndYear(title, year);
            Person p = _personRepository.GetByLastNameAndBirthdate(lastName, birthdate);
            if (f == null)
            {
                throw new Exception("Unknown film");
            }
            if (p == null)
            {
                throw new Exception("Unknown person");
            }
            return GetByFilmIdPersonIdAndRole(f.Id, p.Id, role);
        }

        public FilmPerson GetByTitleYearLastNameBirtdateAndRole(string title, short year, string lastName, string birthdate, string role)
        {
            var f = _filmRepository.GetByTitleAndYear(title, year);
            var p = _personRepository.GetByLastNameAndBirthdate(lastName, birthdate);
            if (f == null)
            {
                throw new Exception("Unknown film");
            }
            if (p == null)
            {
                throw new Exception("Unknown person");
            }
            return GetByFilmIdPersonIdAndRole(f.Id, p.Id, role);
        }

        public async Task<FilmPerson> GetByTitleYearLastNameBirthdateAndRoleAsync(string title, short year, string lastName, string birthdate, string role)
        {
            var f = _filmRepository.GetByTitleAndYear(title, year);
            var p = _personRepository.GetByLastNameAndBirthdate(lastName, birthdate);
            return await GetByFilmIdPersonIdAndRoleAsync(f.Id, p.Id, role);
        }

        FilmPerson IFilmPersonRepository.GetByTitleYearLastNameBirthdateAndRole(string title, short year, string lastName, string birthdate, string role)
        {
            FilmPerson result = null;
            var f = _filmRepository.GetByTitleAndYear(title, year);
            var p = _personRepository.GetByLastNameAndBirthdate(lastName, birthdate);
            if (f != null && p != null)
            {
                result = List().Where(fp => fp.FilmId == f.Id && fp.PersonId == p.Id && fp.Role == role).SingleOrDefault();
            }
            return result;
        }
    }
}
