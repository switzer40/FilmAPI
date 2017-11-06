using FilmAPI.Common.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    public class BaseController<EntityType, InType, OutType> : Controller
        where EntityType : BaseEntity
        where InType : IBaseDto
        where OutType : IKeyedDto
    {
        private readonly EntityService<EntityType, InType, OutType> _service;
        public BaseController(EntityService<EntityType, InType, OutType> service)
        {
            _service = service; 
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]InType t)
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
        public async Task<IActionResult> GetByKey(string key)
        {
            var entity = await _service.GetByKeyAsync(key);
            return Ok(entity);
        }
        [HttpDelete("{key}")]
        public async Task<IActionResult> Remove(string key)
        {
            await _service.RemoveAsync(key);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]InType entity)
        {
            await _service.UpdateAsync(entity);
            return Ok();
        }
    }
}
