using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces.Create
{
    public partial interface ICreateFilmPersonRepository
    {
        FilmPerson Add(FilmPerson fp);
    }
}
