using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmAPI.Controllers
{
    [Route("api/media")]
    public class MediaController : EntitiesController<Medium, MediumViewModel>
    {
        public MediaController(IEntityService<Medium, MediumViewModel> service) : base(service)
        {
        }
    }
}
