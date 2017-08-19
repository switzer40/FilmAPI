using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.ViewModels;

namespace FilmAPI.Services
{
    public class MediumConverter
    {
        private readonly IFilmRepository _filmRepository;
        public MediumConverter(IFilmRepository repo)
        {
            _filmRepository = repo;
        }
        Medium Convert(MediumViewModel model)
        {
            Film f = _filmRepository.GetByTitleAndYear(model.FilmTitle, model.FilmYear);
            return new Medium(f.Id, model.MediumType);
        }
    }
}
