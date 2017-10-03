﻿using FilmAPI.DTOs.Medium;
using FilmAPI.Filters;
using FilmAPI.Filters.Medium;
using FilmAPI.Interfaces;
using FilmAPI.Interfaces.Medium;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers.Medium
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
            var model = await _service.GetBySurrogateKeyAsync(key);
            return Ok(model);
        }
        [HttpPost]
        [ValidateMediumNotDuplicate]
        public async Task<IActionResult> Post([FromBody] KeyedMediumDto model)
        {
            var savedModel = await _service.AddAsync(model);
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