using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("api/films")]
    public class FilmsController : EntitiesController<Film, FilmViewModel>
    {
        public FilmsController(IEntityService<Film, FilmViewModel> service) : base(service)
        {
        }
    }
}
