using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Medium
{
    public interface IMediumDto
    {
        string Title { get; set; }
        short Year { get; set; }
        string MediumType { get; set; }
        string Location { get; set; }
    }
}
