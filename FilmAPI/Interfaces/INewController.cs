using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface INewController 
    {
        Task<List<IKeyedDto>> GetAsync();
        Task<OperationStatus> GetAsync(string key);
        IKeyedDto GetByKeyResult(string key);
        Task<int> GetAsync(int dummy);
        IActionResult HandleError(OperationStatus status);

    }
}
