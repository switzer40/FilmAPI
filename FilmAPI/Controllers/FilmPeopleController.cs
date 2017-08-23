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
    [Route("api/filmpeople")]
    [ValidateModel]
    public class FilmPeopleController : EntitiesController<FilmPerson, FilmPersonViewModel>
    {
        public FilmPeopleController(IEntityService<FilmPerson, FilmPersonViewModel> service) : base(service)
        {
        }
    }
}
