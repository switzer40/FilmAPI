using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Person
{
    public interface IPersonDto
    {
        string FirstMidName { get; set; }
        string LastName { get; set; }
        string Birthdate { get; set; }
    }
}
