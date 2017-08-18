using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using FilmAPI.VviewModls;

namespace FilmAPI.Interfaces
{
    public interface IFilmPersonService : IEntityService<FilmPerson, FilmPersonViewModel>
    {
    }
}
