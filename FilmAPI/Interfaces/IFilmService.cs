using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;

namespace FilmAPI.Interfaces
{
    public interface IFilmService : IEntityService<Film, FilmViewModel>
    {
       
    }
}
