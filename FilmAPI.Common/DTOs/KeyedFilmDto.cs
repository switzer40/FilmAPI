using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class KeyedFilmDto : BaseFilmDto, IKeyedDto
    {
        public KeyedFilmDto(string title,
                            short year,
                            short length = 10,
                            string key = "") : base(title, year, length)
        {
            _key = key;
        }
        private string _key;
        public string Key { get => _key; set => _key = value; }
        public override void Copy(IBaseDto dto)
        {
            if (dto.GetType() == typeof(KeyedFilmDto))
            {
                var that = (KeyedFilmDto)dto;
                var b = that.Restrict();
                base.Copy(b);
                Key = that.Key;
            }
        }
        public override bool Equals(IBaseDto dto)
        {
            var result = base.Equals(dto);
            if (result && (dto.GetType() == typeof(KeyedFilmDto)))
            {
                var that = (KeyedFilmDto)dto;
                result = Key.Equals(that.Key);
            }
            return result;
        }

        public IBaseDto Restrict()
        {
            return new BaseFilmDto(Title, Year, Length);
        }
    }
}
