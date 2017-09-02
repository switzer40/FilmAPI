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
    public class FilmPersonMapper : ModelMapper<FilmPerson, FilmPersonViewModel>, IFilmPersonMapper
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IKeyService _keyService;
        public FilmPersonMapper(IFilmRepository frepo, IPersonRepository prepo, IKeyService keyService)
        {
            _filmRepository = frepo;
            _personRepository = prepo;
            _keyService = keyService;
        }
        public override FilmPersonViewModel Map(FilmPerson e)
        {
            Film f = _filmRepository.GetById(e.FilmId);
            Person p = _personRepository.GetById(e.PersonId);
            var model = new FilmPersonViewModel(f.Title, f.Year, p.LastName, p.BirthdateString, e.Role);
            model.SurrogateKey = _keyService.ConstructFilmPersonSurrogateKey(f.Title, f.Year, p.LastName, p.BirthdateString, e.Role);
            return model;
        }

        public override FilmPerson MapBack(FilmPersonViewModel m)
        {
            throw new NotImplementedException();
        }
    }
}
