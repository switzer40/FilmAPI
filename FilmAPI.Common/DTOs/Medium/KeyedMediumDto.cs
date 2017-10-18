using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs.Medium
{
    public class  KeyedMediumDto : BaseMediumDto, IKeyedDto
    {
        public KeyedMediumDto(string title,
                              short year,
                              string mediumType,
                              string location = "",
                              short length = 0,
                              string key = "") : base(title, year, mediumType, location, length)
        {
            _key = key;
        }
        private string _key;

        public string Key { get => _key; set => _key = value; }
    }
}
