using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces.Create
{
    interface ICreateMediumRepository
    {
        Medium QAdd(Medium m);
    }
}
