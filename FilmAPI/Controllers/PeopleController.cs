﻿using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmAPI.Controllers
{
    [Route("api/people")]
    public class PeopleController : EntitiesController<Person, PersonViewModel>
    {
        public PeopleController(IEntityService<Person, PersonViewModel> service) : base(service)
        {
        }
    }
}
