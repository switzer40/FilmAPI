using FilmAPI.Common.DTOs.FilmPerson;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces.FilmPerson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Services.FilmPerson
{
    public class FilmPersonService : IFilmPersonService
    {
        private readonly IFilmPersonRepository _repository;
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IFilmPersonMapper _mapper;
        private readonly IKeyService _keyService;
        public FilmPersonService(IFilmPersonRepository repo,
                                IFilmRepository frepo,
                                IPersonRepository prepo,
                                IFilmPersonMapper mapper,
                                IKeyService keyService)
        {
            _repository = repo;
            _filmRepository = frepo;
            _personRepository = prepo;
            _mapper = mapper;
            _keyService = keyService;
        }
        public KeyedFilmPersonDto Add(BaseFilmPersonDto b, bool force = false)
        {
               var fpToAdd = (force) ? _mapper.MapBackForce(b) : _mapper.MapBack(b);
               var savedFP = _repository.Add(fpToAdd);
               var key = _keyService.ConstructFilmPersonKey(b.Title, b.Year, b.LastName, b.Birthdate, b.Role);
               var result = new KeyedFilmPersonDto(b.Title, b.Year, b.LastName, b.Birthdate, b.Role, b.Length, b.FirstMidName, key);               
               return result;
        }

        public async Task<KeyedFilmPersonDto> AddAsync(BaseFilmPersonDto b, bool force = false)
        {
            return await Task.Run(() => Add(b, force));
        }

        public void Delete(string key)
        {
            var modelToDelete = GetBySurrogateKey(key);
            var filmToDelete = _mapper.MapBack(modelToDelete);
            _repository.Delete(filmToDelete);
        }

        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => Delete(key));
        }

        public List<KeyedFilmPersonDto> GetAll()
        {
            var filmPeople = _repository.List();
            var baseList = _mapper.MapList(filmPeople);
            var result = new List<KeyedFilmPersonDto>();
            foreach (var item in baseList)
            {
                var key = _keyService.ConstructFilmPersonKey(item.Title, item.Year, item.LastName, item.Birthdate, item.Role);
                var keyedItem = new KeyedFilmPersonDto(item.Title, item.Year, item.LastName, item.Birthdate, item.Role,item.Length, item.FirstMidName, key);                              
                result.Add(keyedItem);
            }
            return result;
        }

        public async Task<List<KeyedFilmPersonDto>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public KeyedFilmPersonDto GetBySurrogateKey(string key)
        {
            var data = _keyService.DeconstructFilmPersonKey(key);
            var f = _filmRepository.GetByTitleAndYear(data.title, data.year);
            var p = _personRepository.GetByLastNameAndBirthdate(data.lastName, data.birthdate);            
            var result = new KeyedFilmPersonDto(f.Title, f.Year, p.LastName, p.BirthdateString, data.role,  f.Length, p.FirstMidName, key);
            return result;
        }

        public async Task<KeyedFilmPersonDto> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }

        public void Update(BaseFilmPersonDto m)
        {
            var filmPersonToUpdate = _mapper.MapBack(m);
            _repository.Update(filmPersonToUpdate);
        }

        public async Task UpdateAsync(BaseFilmPersonDto m)
        {
            await Task.Run(() => Update(m));
        }
    }
}
