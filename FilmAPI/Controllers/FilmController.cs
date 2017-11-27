using FilmAPI.Interfaces.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using FilmAPI.Interfaces.Services;
using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Filters;

namespace FilmAPI.Controllers
{
    [Route(FilmConstants.FilmUri)]
    [ValidateModel]
    public class FilmController : Controller,  IFilmController
    {
        private readonly IFilmService _service;
        public FilmController(IFilmService service)
        {
            _service = service;
        }
        [HttpDelete("{key}")]
        [ValidateFilmExists]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            IActionResult retVal = Ok();
            var status = await _service.DeleteAsync(key);
            switch (status)
            {
                case OperationStatus.OK:
                    retVal = Ok();
                    break;
                case OperationStatus.BadRequest:
                    retVal = BadRequest();
                    break;
                case OperationStatus.NotFound:
                    retVal = NotFound();
                    break;
                default:
                    break;
            }
            return retVal;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var films = await _service.GetAllAsync();
            return Ok(films);
        }
        [HttpGet("{key}")]
        [ValidateFilmExists]
        public async Task<IActionResult> GetByKeyAsync(string key)
        {
            var film = await _service.GetByKeyAsync(key);
            return Ok(film);
        }
        [HttpPost]
        [ValidateFilmNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody]BaseFilmDto model)
        {
            var status = await _service.AddAsync(model);
            switch (status)
            {
                case OperationStatus.OK:
                    KeyedFilmDto savedFilm = (KeyedFilmDto)_service.Result();
                    return Ok(savedFilm);
                case OperationStatus.BadRequest:
                    return BadRequest();
                case OperationStatus.NotFound:
                    return NotFound();
                default:
                    throw new Exception("Unknown status");
            }            
        }
        [HttpPut]
        [ValidateFilmToUpdateExists]
        public async Task<IActionResult> PutAsync([FromBody]BaseFilmDto model)
        {
            IActionResult retVal =Ok();
            var status = await _service.UpdateAsync(model);
            switch (status)
            {
                case OperationStatus.OK:
                    retVal =Ok();
                    break;
                case OperationStatus.BadRequest:
                    retVal = BadRequest();
                    break;
                case OperationStatus.NotFound:
                    retVal = NotFound();
                    break;
                default:
                    break;
            }
            return retVal;
        }
    }
}
