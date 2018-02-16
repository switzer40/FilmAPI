using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IPersonService : IService<Person>
    {

        OperationResult<IKeyedDto> GetByLastNameAndBirthdate(string lastName, string birthdate);
        Task<OperationResult<IKeyedDto>> GetByLastNameAndBirthdateAsync(string lastName, string birthdate);
    }
}
