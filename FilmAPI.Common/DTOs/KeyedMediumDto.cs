using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class KeyedMediumDto : BaseMediumDto, IKeyedDto<Medium>
    {
        public KeyedMediumDto(string title, short year, string mediumType, string location = "", string key = "") :base(title, year, mediumType, location)
        {
            _key = key;
        }
        private string _key;

        public string Key { get => _key; set => _key = value; }
    }
}
