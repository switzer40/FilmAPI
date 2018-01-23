using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class KeyedFilmPersonDto : BaseFilmPersonDto, IKeyedDto
    {
        public KeyedFilmPersonDto(string title,
                                  short year,
                                  string lastName,
                                  string birthdate,
                                  string role,
                                  string key = "") : base(title, year, lastName, birthdate, role)
        {
            _key = key;
        }

        private string _key;
        public string Key { get => _key; set => _key = value; }
        public override void Copy(IBaseDto dto)
        {
            if (dto.GetType() == typeof(KeyedFilmPersonDto))
            {
                var that = (KeyedFilmPersonDto)dto;
                Key = that.Key;
            }
        }
        public override bool Equals(IBaseDto dto)
        {
            var result = base.Equals(dto);
            if (result && (dto.GetType() == typeof(KeyedFilmPersonDto)))
            {
                var that = (KeyedFilmPersonDto)dto;
                var b = that.Restrict();
                base.Copy(b);
                result = result && (this.Key.Equals(that.Key));
            }
            return result;
        }

        public IBaseDto Restrict()
        {
            return new BaseFilmPersonDto(Title, Year, LastName, Birthdate, Role);
        }
    }
}
