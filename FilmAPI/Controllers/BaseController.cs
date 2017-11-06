using FilmAPI.Common.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Interfaces.Film;
using FilmAPI.Interfaces;
using FilmAPI.Filters;

namespace FilmAPI.Controllers
{
    public class BaseController<EntityType, InType, OutType> : Controller
        where EntityType : BaseEntity
        where InType : IBaseDto
        where OutType : IKeyedDto
    {
        private readonly IEntityService<EntityType, InType, OutType> _service;
               
        public BaseController(IEntityService<EntityType, InType, OutType> service)
        {
            _service = service;
        }

        [HttpPost]
        [ValidateEntityIsNotDuplicate]
        public async Task<IActionResult> Post([FromBody]InType t)
        {
            var entity = await _service.AddAsync(t);
            return Ok(entity);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }
        [HttpGet("{key}")]
        [ValidateEntityExists]
        public async Task<IActionResult> Get(string key)
        {
            var entity = await _service.GetByKeyAsync(key);
            return Ok(entity);
        }
        [HttpDelete("{key}")]
        [ValidateEntityExists]
        public async Task<IActionResult> Delete(string key)
        {
            await _service.RemoveAsync(key);
            return Ok();
        }
        [HttpPut]
        [ValidateEntityToUpdateExists]
        public async Task<IActionResult> Put([FromBody]InType t)
        {
            await _service.UpdateAsync(t);
            return Ok();
        }
    }
}
