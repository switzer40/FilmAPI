using FilmAPI.Common.Interfaces;
using FilmAPI.Core.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Controllers
{
    public interface IController<T> where T : BaseEntity
    {
        Task<IActionResult> GetAsync();
        Task<IActionResult> GetByKeyAsync(string key);
        Task<IActionResult> PostAsync(IBaseDto<T> b);
        Task<IActionResult> PutAsync(IBaseDto<T> b);
        Task<IActionResult> DeleteAsync(string key);
    }
}
