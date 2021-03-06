﻿using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Filters;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("/api/FilmPerson/")]
    public class FilmPersonController : BaseController<KeyedFilmPersonDto>
    {
        private readonly IFilmPersonService _service;
        public FilmPersonController(IFilmPersonService service)
        {
            _service = service;
        }
        [HttpDelete("ClearAll")]
        public async Task<OperationStatus> DeleteAsync()
        {
            return await _service.ClearAllAsync();            
        }
        [HttpDelete("Delete/{key}")]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            var res = await _service.DeleteAsync(key);
            return StandardReturn(res);
        }
        [HttpGet("Count")]
        public async Task<OperationResult<int>> GetAsync(int dummy)
        {
            return await _service.CountAsync();            
        }
        [HttpGet("GetAll")]
        public async Task<OperationResult<List<IKeyedDto>>> GetAsync(int pageIndex = 0, int pageSize = 4)
        {
            return await _service.GetAllAsync(pageIndex, pageSize);            
        }
        [HttpGet("GetByKey/{key}")]
        [ValidateFilmPersonExists]
        public async Task<OperationResult<IKeyedDto>> GetAsync(string key)
        {
            return await _service.GetByKeyAsync(key);           
        }
        [HttpPost("Add")]
        [ValidateFilmPersonNotDuplicate]
        public async Task<OperationResult<IKeyedDto>> PostAsync([FromBody]BaseFilmPersonDto model)
        {
            return await _service.AddAsync(model);            
        }
        [HttpPut("Edit")]
        [ValidateFilmPersonToUpdateExists]
        public async Task<OperationStatus> PutAsync([FromBody]BaseFilmPersonDto model)
        {
            return await _service.UpdateAsync(model);                        
        }
    }
}
