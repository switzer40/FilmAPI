using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FilmAPI.Common.Constants;
using FilmAPI.Filters;
using FilmAPI.Interfaces.Services;
using FilmAPI.Filters.Medium;

namespace FilmAPI.Controllers
{
    [Route(FilmConstants.MediumUri)]
    [ValidateModel]
    public class MediumController : BaseController<Medium>, IMediumController
    {
        public MediumController(IMediumService service) : base(service)
        {
        }
        [HttpDelete("{key}")]
        [ValidateMediumExists]
        public override async Task<IActionResult> DeleteAsync(string key)
        {
            await _service.DeleteAsync(key);
            return Ok();
        }
        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            var media = await _service.GetAllAsync();
            return Ok(media);
        }
        [HttpGet("{key}")]
        [ValidateMediumExists]
        public override async Task<IActionResult> GetByKeyAsync(string key)
        {
            var medium = await _service.GetByKeyAsync(key);
            return Ok(medium);
        }
        [HttpPost]
        [ValidateMediumNotDuplicate]
        public override async Task<IActionResult> PostAsync([FromBody] IBaseDto<Medium> b)
        {
            var savedMedium = await _service.AddAsync(b);
            return Ok(savedMedium);
        }
        [HttpPut]
        [ValidateMediumToUpdateExists]
        public override async Task<IActionResult> PutAsync([FromBody] IBaseDto<Medium> b)
        {
            await _service.UpdateAsync(b);
            return Ok();
        }
    }
}
