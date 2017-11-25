﻿using FilmAPI.Common.DTOs;
using FilmAPI.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Controllers
{
    public interface IFilmController
    {
        [HttpDelete("{key}")]
        Task<IActionResult> DeleteAsync(string key);

        [HttpGet]
        Task<IActionResult> GetAsync();

        [HttpGet("{key}")]
        Task<IActionResult> GetByKeyAsync(string key);
        [HttpPost]
        Task<IActionResult> PostAsync([FromBody]BaseFilmDto b);
        [HttpPut]
        Task<IActionResult> PutAsync([FromBody] BaseFilmDto b);
    }
}