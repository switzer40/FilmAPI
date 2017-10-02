using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IFilmDto
    {
        string Title { get; set; }
        short Year { get; set; }
        short Length { get; set; }
    }
}
