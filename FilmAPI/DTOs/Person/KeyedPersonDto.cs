using FilmAPI.Interfaces.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.DTOs.Person
{
    public class KeyedPersonDto : BasePersonDto, IPersonDto
    {     
        public KeyedPersonDto(string lastName, string birthdate, string key, string firstMidName = "") : base(lastName, birthdate, firstMidName)
        {
            SurrogateKey = key;
        }

        public string SurrogateKey { get; set; }
    }
}
