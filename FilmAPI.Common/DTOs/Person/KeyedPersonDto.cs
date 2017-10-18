using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs.Person
{
    public class KeyedPersonDto : BasePersonDto, IKeyedDto
    {
        public KeyedPersonDto(string lastName, string birthdate, string firstMidName = "", string key = "") : base(lastName, birthdate, firstMidName)
        {
            _key = key;
        }
        private string _key;

        public string Key { get => _key; set => _key = value; }
    }
}
