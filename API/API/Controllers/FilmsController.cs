using API.ViewModels;
using API.Filters;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ValidateModel]
    public class FilmsController : BaseController<Film, FilmViewModel>
    {
        public FilmsController(IRepository<Film> repository, IMapper mapper) : base(repository, mapper)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rawFilms = await _repository.ListAsync();
            return Ok(_mapper.Map<FilmViewModel>(rawFilms));
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            
            var rawFilm = await _repository.GetBySurrogateKeyAsync(key);
            return Ok(_mapper.Map<FilmViewModel>(rawFilm));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rawFilm = await _repository.GetByIdAsync(id);
            return Ok(_mapper.Map<FilmViewModel>(rawFilm));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FilmViewModel model)
        {
            var filmToStore = _mapper.Map<Film>(model);
            var storedFilm = await _repository.AddAsync(filmToStore);
            var result = _mapper.Map<FilmViewModel>(storedFilm);
            return Ok(result);
        }                   
    }
}
