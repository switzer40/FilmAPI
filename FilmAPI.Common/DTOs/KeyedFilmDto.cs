using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class KeyedFilmDto : BaseFilmDto, IKeyedDto<Film>
    {
        public KeyedFilmDto(string title, short year, short length = 0, string key = "") : base(title, year, length)
        {
            _key = key;
        }
        private string _key;

        public string Key { get => _key; set => _key = value; }
    }
}
