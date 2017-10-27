using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces.Update
{
    public partial interface IUpdateFilmRepository
    {
        void Update(Film f);
    }
}
