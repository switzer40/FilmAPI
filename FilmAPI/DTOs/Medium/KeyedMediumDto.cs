using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Interfaces.Medium;

namespace FilmAPI.DTOs.Medium
{
    public class KeyedMediumDto : BaseMediumDto, IMediumDto
    {
        public KeyedMediumDto()
        {
        }
        public KeyedMediumDto(string title, short year, string mediumType, string location = "") : base(title, year, mediumType, location)
        {
        }
        public string SurrogateKey { get; set; }
    }
}
