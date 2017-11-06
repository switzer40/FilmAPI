using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FilmAPI.Infrastructure.Data;
using System.Linq;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Core.Exceptions;

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
            return List(spec).SingleOrDefault();
        }

        public async Task<FilmPerson> GetByFilmIdPersonIdAndRoleAsync(int filmId, int personId, string role)
        {
            var spec = new FilmPersonByFilmIdPersonIdAndRole(filmId, personId, role);
            var candidates = await ListAsync(spec);
            var uniqueCandidate = candidates.Single();
            return uniqueCandidate;
        }

        public override FilmPerson GetByKey(string key)
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
                throw new UnknownFilmException(title, year);
            }
            if (p == null)
            {
                throw new UnknownPersonException(lastName, birthdate);
            }
            return new FilmPerson(f.Id, p.Id, role);
        }

        public override FilmPerson GetStoredEntity(FilmPerson t)
        {
            return GetByFilmIdPersonIdAndRole(t.FilmId, t.PersonId, t.Role);
        }

        public override void Update(FilmPerson t)
        {
            var storedFilmPerson = GetByFilmIdPersonIdAndRole(t.FilmId, t.PersonId, t.Role);
            storedFilmPerson.Copy(t);
            Save();
        }
    }
}
