using FilmAPI.Common.DTOs.Medium;
using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Medium;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("api/media")]
    public class MediaController : BaseController<Medium, BaseMediumDto, KeyedMediumDto>
    {        
        public MediaController(IMediumService service) : base(service)
        {
        }
    }
}
