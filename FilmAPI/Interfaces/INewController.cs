using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IController 
    {
        Task<ActionResult> GetAsync(); // GetAbsolutelyAll
        Task<IActionResult> GetAsync(int dummy); // Count
        Task<IActionResult> GetAsync(int pageIndex, int pageSize); // GetAll
        Task<IActionResult> GetAsync(string key); // GetByKey
        Task<IActionResult> DeleteAsync(); // ClearAll
        Task<IActionResult> DeleteAsync(string key); // Delete

    }
}
