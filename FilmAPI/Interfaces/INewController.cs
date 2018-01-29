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
        Task<IActionResult> GetAsync(int dummy); // Count
        Task<IActionResult> GetAsync(); // GetAll
        Task<IActionResult> GetAsync(string key); // GetByKey
        Task<IActionResult> DeleteAsync(); // ClearAll
        Task<IActionResult> DeleteAsync(string key); // Delete

    }
}
