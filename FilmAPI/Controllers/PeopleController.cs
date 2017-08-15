using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("api/people")]
    public class PeopleController : Controller
    {
        private readonly IPersonService _service;
        public PeopleController(IPersonService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var models = await _service.GetAllAsync();
            return Ok(models);
        }
    }
}
