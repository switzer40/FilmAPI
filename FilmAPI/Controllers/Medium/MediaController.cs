using FilmAPI.Common.DTOs.Medium;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Filters;
using FilmAPI.Filters.Medium;
using FilmAPI.Interfaces;
using FilmAPI.Interfaces.Medium;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilmAPI.Controllers.Medium
{
    [Route(FilmConstants.MediumUri)]
    [ValidateModel]
    public class MediaController : Controller
    {
        private readonly IMediumService _service;
        private readonly IKeyService _keyService;
        private bool _force = false;
        public MediaController(IMediumService service, IKeyService keyService)
        {
            _force = FilmConstants.Force;
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
            var model = await _service.GetBySurrogateKeyAsync(key);
            return Ok(model);
        }
        [HttpPost]
        [ValidateMediumNotDuplicate]
        public async Task<IActionResult> Post([FromBody] KeyedMediumDto model)
        {
            var savedModel = await _service.AddAsync(model, _force);
            return Ok(savedModel);
        }
        [HttpPut]
        [ValidateMediumToUpdateExists]
        public async Task<IActionResult> Put([FromBody] KeyedMediumDto model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }
        [HttpDelete("{key}")]
        [ValidateMediumExists]
        public async Task<IActionResult>Delete(string key)
        {
            await _service.DeleteAsync(key);
            return Ok();
        }
    }
}
