using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;

namespace FilmAPI.Interfaces
{
    public interface IPersonService : IEntityService<Person, PersonViewModel>
    {
    }
}
