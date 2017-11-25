using FilmAPI.Interfaces.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Services;
using FilmAPI.Filters.Medium;
using FilmAPI.Common.Constants;
using FilmAPI.Filters;
using FilmAPI.Common.DTOs;

namespace FilmAPI.Controllers
{
    [Route(FilmConstants.MediumUri)]
    [ValidateModel]
    public class MediumController : Controller, IMediumController
    {
        private readonly IMediumService _service;
        public MediumController(IMediumService service)
        {
            _service = service;
        }
        [HttpDelete("{key}")]
        [ValidateMediumExists]
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
            var media = await _service.GetAllAsync();
            return Ok(media);
        }
        [HttpGet("{key}")]
        [ValidateMediumExists]
        public async Task<IActionResult> GetByKeyAsync(string key)
        {
            var medium = await _service.GetByKeyAsync(key);
            return Ok(medium);
        }
        [HttpPost]
        [ValidateMediumNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody]BaseMediumDto b)
        {
            var savedMedium = await _service.AddAsync(b);
            return Ok(savedMedium);
        }
        [HttpPut]
        [ValidateMediumToUpdateExists]
        public async Task<IActionResult> PutAsync([FromBody]BaseMediumDto b)
        {
            await _service.UpdateAsync(b);
            return Ok();
        }
    }
}
