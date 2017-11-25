using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Filters;
using FilmAPI.Filters.FilmPerson;
using FilmAPI.Interfaces.Controllers;
using FilmAPI.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route(FilmConstants.FilmPersonUri)]
    [ValidateModel]
    public class FilmPersonController : Controller, IFilmPersonController
    {
        private readonly IFilmPersonService _service;
        public FilmPersonController(IFilmPersonService service)
        {
            _service = service;
        }
        [HttpDelete("{key}")]
        [ValidateFilmPersonExists]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            IActionResult result = Ok();
            var status = await _service.DeleteAsync(key);
            switch (status)
            {
                case OperationStatus.OK:
                    result = Ok();
                    break;
                case OperationStatus.BadRequest:
                    result = BadRequest();
                    break;
                case OperationStatus.NotFound:
                    result = NotFound();
                    break;
                default:
                    break;
            }
            return result;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var filmPeople = await _service.GetAllAsync();
            return Ok(filmPeople);
        }
        [HttpGet("{key}")]
        [ValidateFilmPersonExists]
        public async Task<IActionResult> GetByKeyAsync(string key)
        {
            var filmPerson = await _service.GetByKeyAsync(key);
            return Ok(filmPerson);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BaseFilmPersonDto b)
        {
            var savedFilmPerson = await _service.AddAsync(b);
            return Ok(savedFilmPerson);
        }
    }
}
