using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IFilmPersonService : IService<FilmPerson>
    {
        OperationResult<IKeyedDto> GetByTitleYearLastNameBirthdateAndRole(string title,
                                                                          short year,
                                                                          string lastName,
                                                                          string birthdate,
                                                                          string role);
        Task<OperationResult<IKeyedDto>> GetByTitleYearLastNameBirthdateAndRoleAsync(string title,
                                                                                     short year,
                                                                                     string lastName,
                                                                                     string birthdate, 
                                                                                     string role);
    }
}
