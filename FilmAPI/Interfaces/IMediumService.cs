using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IMediumService : IService<Medium>
    {
        OperationResult<IKeyedDto> GetByTitleYearAndMediumType(string title,
                                                               short year,
                                                               string mediumType);
        Task<OperationResult<IKeyedDto>> GetByTitleYearAndMediumTypeAsync(string title,
                                                                          short year,
                                                                          string mediumType);
    }
}
