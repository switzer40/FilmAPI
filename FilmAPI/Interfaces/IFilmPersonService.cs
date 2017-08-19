using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;

namespace FilmAPI.Interfaces
{
    public interface IFilmPersonService : IEntityService<FilmPerson, FilmPersonViewModel>
    {
    }
}
