using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class KeyedFilmPersonDto : BaseFilmPersonDto, IKeyedDto<FilmPerson>
    {
        public KeyedFilmPersonDto(string title,
                                  short year,
                                  string lastName,
                                  string birthdate,
                                  string role,
                                  string key= "") : base(title, year, lastName, birthdate, role)
        {
            _key = key;
        }
        private string _key;

        public string Key { get => _key; set => _key = value; }
    }
}
