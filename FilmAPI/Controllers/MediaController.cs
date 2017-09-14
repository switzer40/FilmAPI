using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FilmAPI.Filters;

namespace FilmAPI.Controllers
{
    [Route("api/media")]
    [ValidateModel]
    public class MediaController : Controller
    {        
        private readonly IMediumService _service;
        private readonly IKeyService _keyService;
        public MediaController(IMediumService service, IKeyService keyService)
        {
            _service = service;
            _keyService = keyService;
        }

       

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var media = await _service.GetAllAsync();
            return Ok(media);
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            MediumViewModel model = null;
            try
            {
                model = await _service.GetBySurrogateKeyAsync(key);
            }
            catch
            {
                return BadRequest(key);
            }
            return Ok(model);            
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MediumViewModel model)
        {
            model.SurrogateKey = _keyService.ConstructMediumSurrogateKey(model.FilmTitle, model.FilmYear, model.MediumType);
            var savedMedium =await _service.AddAsync(model.SurrogateKey);
            return Ok(savedMedium);
        }
        [HttpPut("{key}")]
        [ValidateMediumExists]
        public async Task<IActionResult> Put(string key, [FromBody] MediumViewModel model)
        {
            model.SurrogateKey = _keyService.ConstructMediumSurrogateKey(model.FilmTitle, model.FilmYear, model.MediumType);model.SurrogateKey = _keyService.ConstructMediumSurrogateKey(model.FilmTitle, model.FilmYear, model.MediumType);
            await _service.UpdateAsync(model.SurrogateKey);
            return Ok();
        }
        [HttpDelete("{key}")]
        [ValidateMediumExists]
        public async Task<IActionResult> Delete(string key)
        {
            try
            {
                await _service.DeleteAsync(key);
            }
            catch (Exception e)
            {
                return BadRequest(key);
            }
            return Ok();
        }
    }
}
