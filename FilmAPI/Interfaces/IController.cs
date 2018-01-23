using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IController
    {
        Task<OperationStatus> GetAsync();
        Task<OperationStatus> GetAsync(int dummy);
        IActionResult GetAsync(string key);
        List<IKeyedDto> GetAllResult();
        IKeyedDto GetResult(string key);
        IActionResult HandleError(OperationStatus status);
    }
}
