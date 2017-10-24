using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces.Create
{
    public partial interface ICreateFilmRepository
    {
        Film Add(Film f);
    }
}
