using FilmAPI.Common.DTOs.Person;
using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    public class PeopleController : BaseController<Person, BasePersonDto, KeyedPersonDto>
    {
        public PeopleController(IPersonService service) : base(service)
        {
        }
    }
}
