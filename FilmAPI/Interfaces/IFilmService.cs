using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IFilmService : IService<Film>
    {
        OperationResult<IKeyedDto> GetByTitleAndYear(string title, short year);
        Task<OperationResult<IKeyedDto>> GetByTitleAndYearAsync(string title, short year);
    }
}
