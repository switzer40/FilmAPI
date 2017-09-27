using FilmAPI.ViewModels;
using System;
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
        [ValidateMediumExists]
        public async Task<IActionResult> Get(string key)
        {
            MediumViewModel model = await _service.GetBySurrogateKeyAsync(key);            
            return Ok(model);            
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MediumViewModel model)
        {            
            var savedMedium =await _service.AddForceAsync(model);
            return Ok(savedMedium);
        }
        [HttpPut]
        [ValidateMediumToUpdateExists]
        public async Task<IActionResult> Put([FromBody] MediumViewModel model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }
        [HttpPut("{key}")]
        [ValidateMediumExists]
        public async Task<IActionResult> Put(string key, [FromBody] MediumViewModel model)
        {            
            await _service.UpdateAsync(model);
            return Ok();
        }
        [HttpDelete("{key}")]
        [ValidateMediumExists]
        public async Task<IActionResult> Delete(string key)
        {
            await _service.DeleteAsync(key);            
            return Ok();
        }
    }
}
