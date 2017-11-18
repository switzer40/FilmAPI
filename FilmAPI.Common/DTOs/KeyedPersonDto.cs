using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class KeyedPersonDto : BasePersonDto, IKeyedDto<Person>
    {
        public KeyedPersonDto(string lastName,
                              string birthdate,
                              string firstMidName = "",
                              string key = "") : base(lastName, birthdate, firstMidName)
        {
            _key = key;
        }
        private string _key;

        string IKeyedDto<Person>.Key { get => _key; set => _key = value; }
    }
}
