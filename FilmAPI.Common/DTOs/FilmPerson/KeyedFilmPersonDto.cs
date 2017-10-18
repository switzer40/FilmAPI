using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs.FilmPerson
{
    public class KeyedFilmPersonDto : BaseFilmPersonDto, IKeyedDto
    {
        public KeyedFilmPersonDto(string title,
                                  short year,
                                  string lastName,
                                  string birthdate,
                                  string role,
                                  short length = 0,
                                  string firstMidName = "",
                                  string key = "") : base(title, year, lastName, birthdate, role)
        {
            Length = length;
            FirstMidName = firstMidName;
            _key = key;
        }
        private string _key;

        public string Key { get => _key; set => _key = value; }
    }
}
