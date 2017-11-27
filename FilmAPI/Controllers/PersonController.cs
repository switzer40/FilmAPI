using FilmAPI.Interfaces.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Services;
using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Filters;
using FilmAPI.Filters.Person;
using FilmAPI.Common.Validators;

namespace FilmAPI.Controllers
{
    [Route(FilmConstants.PersonUri)]
    [ValidateModel]
    public class PersonController : Controller, IPersonController
    {
        private readonly IPersonService _service;
        public PersonController(IPersonService service)
        {
            _service = service;
        }
        [HttpDelete("{key}")]
        [ValidatePersonExists]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            IActionResult result = Ok();
            var status =  await _service.DeleteAsync(key);
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
            var people = await _service.GetAllAsync();
            return Ok(people);
        }
        [HttpGet("{key}")]
        [ValidatePersonExists]
        public async Task<IActionResult> GetByKeyAsync(string key)
        {
            var person = await _service.GetByKeyAsync(key);
            return Ok(person);
        }
        [HttpPost]
        [ValidatePersonNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody]BasePersonDto model)
        {
            var status = await _service.AddAsync(model);
            switch (status)
            {
                case OperationStatus.OK:
                    KeyedPersonDto savedPerson = (KeyedPersonDto)_service.Result();
                    return Ok(savedPerson);
                case OperationStatus.BadRequest:
                    return BadRequest();
                case OperationStatus.NotFound:
                    return NotFound();
                default:
                    throw new Exception("Unknown status");
            }
        }
        [HttpPut]
        [ValidatePersonToUpdateExists]
        public async Task<IActionResult> PutAsync([FromBody]BasePersonDto b)
        {
            await _service.UpdateAsync(b);
            return Ok();
        }
    }
}
