using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces.Create
{
    interface ICreatePersonRepository
    {
        Person Add(Person p);
    }
}
