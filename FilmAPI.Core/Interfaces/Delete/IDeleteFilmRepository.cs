using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces.Delete
{
    public partial interface IDeleteFilmRepository
    {
        void Delete(int id);
    }
}
