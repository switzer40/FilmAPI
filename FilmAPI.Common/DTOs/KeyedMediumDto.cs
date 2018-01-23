using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class KeyedMediumDto : BaseMediumDto, IKeyedDto
    {
        public KeyedMediumDto(string title,
                              short year,
                              string mediumType,
                              string location = "",
                              bool german = true,
                              string key = "") : base(title, year, mediumType, location, german)
        {
            _key = key;
        }
        private string _key;
        public string Key { get => _key; set => _key = value; }
        public override void Copy(IBaseDto dto)
        {

            if (dto.GetType() == typeof(KeyedMediumDto))
            {
                var that = (KeyedMediumDto)dto;
                var b = that.Restrict();
                base.Copy(b);
                Key = that.Key;
            }
        }
        public override bool Equals(IBaseDto dto)
        {
            var result = base.Equals(dto);
            if (result && (dto.GetType() == typeof(KeyedMediumDto)))
            {
                var that = (KeyedMediumDto)dto;
                result = Key.Equals(that.Key);
            }
            return result;
        }

        public IBaseDto Restrict()
        {
            return new BaseMediumDto(Title, Year, MediumType, Location, HasGermanSubtitles);
        }
    }
}
